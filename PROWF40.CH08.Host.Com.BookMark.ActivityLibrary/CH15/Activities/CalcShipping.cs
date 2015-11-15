using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH15.Activities_designer;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH15.Activities
{

    [Designer(typeof(CalcShippingDesigner))]
    public sealed class CalcShipping : CodeActivity<Decimal>
    {
        public InArgument<Int32> Weight { get; set; }
        public InArgument<Decimal> OrderTotal { get; set; }
        public InArgument<String> ShipVia { get; set; }
        protected override Decimal Execute(CodeActivityContext context)
        {
            throw new NotImplementedException();
        }

    }
}
