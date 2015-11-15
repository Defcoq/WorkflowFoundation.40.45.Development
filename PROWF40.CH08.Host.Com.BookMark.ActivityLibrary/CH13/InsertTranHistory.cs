using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using PROWF40.CH13.AdventureWorksAccess;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH13
{

    public sealed class InsertTranHistory : CodeActivity
    {
        public InArgument<SalesOrderDetail> SalesDetail { get; set; }


        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            SalesOrderDetail salesDetail = SalesDetail.Get(context);
            using (AdventureWorksDataContext dc = new AdventureWorksDataContext())
            {
                var historyRow = new TransactionHistory();
                historyRow.ProductID = salesDetail.ProductID;
                historyRow.ModifiedDate = DateTime.Now;
                historyRow.Quantity = salesDetail.OrderQty;
                historyRow.TransactionDate = salesDetail.ModifiedDate;
                historyRow.TransactionType = 'S';
                historyRow.ReferenceOrderID = salesDetail.SalesOrderID;
                historyRow.ReferenceOrderLineID = salesDetail.SalesOrderDetailID;
                dc.TransactionHistories.InsertOnSubmit(historyRow);
                dc.SubmitChanges();
                Console.WriteLine("Product {0}: Added history for Qty of {1} ",salesDetail.ProductID, salesDetail.OrderQty);
            }

        }
    }
}
