using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using PROWF40.CH13.AdventureWorksAccess;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH13
{

    public sealed class ExternalVoid : CodeActivity
    {
        public InArgument<Int32> SalesOrderId { get; set; }
        public InArgument<List<SalesOrderDetail>> OrderDetail { get; set; }


        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            String operation = "void previous sale";
            Console.WriteLine(
            "Order Id {0}: Notifying external system to {1}",
            SalesOrderId.Get(context), operation);
            foreach (SalesOrderDetail detail in OrderDetail.Get(context))
            {
                Console.WriteLine("Product {0}: {1}",
                detail.ProductID, operation);
            }

        }
    }
}
