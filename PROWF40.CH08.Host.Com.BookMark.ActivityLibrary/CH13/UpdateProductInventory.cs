using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using PROWF40.CH13.AdventureWorksAccess;
using System.Transactions;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH13
{

    public sealed class UpdateProductInventory : AsyncCodeActivity
    {
        public InArgument<SalesOrderDetail> SalesDetail { get; set; }


        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            DependentTransaction dependentTran = null;
            if (Transaction.Current != null)
            {
                dependentTran = Transaction.Current.DependentClone(
                DependentCloneOption.BlockCommitUntilComplete);
            }

            Action<DependentTransaction, SalesOrderDetail> asyncWork =(dt, sale) => UpdateInventory(dt, sale);
            context.UserState = asyncWork;
            return asyncWork.BeginInvoke(dependentTran, SalesDetail.Get(context), callback, state);


        }

        private void UpdateInventory(DependentTransaction dt, SalesOrderDetail salesDetail)
        {
            try
            {
                using (AdventureWorksDataContext dc = new AdventureWorksDataContext())

                {
                    using (TransactionScope scope = (dt != null ?new TransactionScope(dt) :new TransactionScope(TransactionScopeOption.Suppress)))
                    {
                        var inventoryRow =
                                    (from pi in dc.ProductInventories
                                    where pi.ProductID == salesDetail.ProductID
                                    && pi.LocationID == 7 //finished goods storage
                                    select pi).SingleOrDefault();
                        if (inventoryRow != null)
                        {
                            inventoryRow.Quantity -= salesDetail.OrderQty;
                            inventoryRow.ModifiedDate = DateTime.Now;
                            Console.WriteLine(
                            "Product {0}: Reduced by {1}",
                            inventoryRow.ProductID, salesDetail.OrderQty);
                            dc.SubmitChanges();
                        }
                        scope.Complete();


                    }
                }
            }
                
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                //the ambient transaction will block on complete
                if (dt != null)
                {
                    dt.Complete();
                    dt.Dispose();
                }

            }
        }


        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            ((Action<DependentTransaction, SalesOrderDetail>)context.UserState).EndInvoke(result);

        }
    }
}
