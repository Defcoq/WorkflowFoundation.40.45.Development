using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using PROWF40.CH13.AdventureWorksAccess;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH13
{

    public sealed class GetOrderDetail : AsyncCodeActivity
    {
        public InArgument<Int32> SalesOrderId { get; set; }
        public OutArgument<List<SalesOrderDetail>> OrderDetail { get; set; }




        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            Func<Int32, List<SalesOrderDetail>> asyncWork = orderId => RetrieveOrderDetail(orderId);
            context.UserState = asyncWork;
            return asyncWork.BeginInvoke(SalesOrderId.Get(context), callback, state);

        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            List<SalesOrderDetail> orderDetail =((Func<Int32, List<SalesOrderDetail>>)context.UserState).EndInvoke(result);
            if (orderDetail != null)
            {
                OrderDetail.Set(context, orderDetail);
            }

        }

        private List<SalesOrderDetail> RetrieveOrderDetail(Int32 salesOrderId)
        {
            List<SalesOrderDetail> result = new List<SalesOrderDetail>();
            using (AdventureWorksDataContext dc = new AdventureWorksDataContext())
            {
                var salesDetail =
                (from sd in dc.SalesOrderDetails
                 where sd.SalesOrderID == salesOrderId
                 select sd).ToList();
                if (salesDetail != null && salesDetail.Count > 0)
                {
                    result = salesDetail;
                }
            }
            return result;
        }

    }
}
