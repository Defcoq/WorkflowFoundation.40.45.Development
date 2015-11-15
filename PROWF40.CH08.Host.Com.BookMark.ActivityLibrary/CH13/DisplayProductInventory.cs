using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using PROWF40.CH13.AdventureWorksAccess;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH13
{

    public sealed class DisplayProductInventory : AsyncCodeActivity
    {
        public InArgument<String> Description { get; set; }
        public InArgument<SalesOrderDetail> SalesDetail { get; set; }
        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            Action<SalesOrderDetail, String> asyncWork =
            (sale, desc) => DisplayInventory(sale, desc);
            context.UserState = asyncWork;
            return asyncWork.BeginInvoke(
            SalesDetail.Get(context), Description.Get(context),
            callback, state);
        }
        protected override void EndExecute(
        AsyncCodeActivityContext context, IAsyncResult result)
        {
            ((Action<SalesOrderDetail, String>)
            context.UserState).EndInvoke(result);
        }

        private void DisplayInventory(SalesOrderDetail salesDetail, String desc)
        {
            using (AdventureWorksDataContext dc = new AdventureWorksDataContext())
            {
                var inventoryRow =(from pi in dc.ProductInventories
                 where pi.ProductID == salesDetail.ProductID
                 && pi.LocationID == 7 //finished goods storage
                 select pi).SingleOrDefault();
                Boolean historyRowFound =
                (from th in dc.TransactionHistories
                 where th.ProductID == salesDetail.ProductID
                 && (DateTime.Now - th.ModifiedDate < new TimeSpan(0, 0, 3))
                 select th).Any();
                if (inventoryRow != null)
                {
                    Console.WriteLine("Product {0}: {1} - {2} - {3}",
                    inventoryRow.ProductID, inventoryRow.Quantity, desc,
                    (historyRowFound ? "History Row Found" : "No History"));
                }
            }

        }

    }
}
