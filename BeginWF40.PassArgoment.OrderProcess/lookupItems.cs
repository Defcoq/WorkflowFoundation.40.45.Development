using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace OrderProcess
{

    public sealed class lookupItems : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ItemCode { get; set; }
        public OutArgument <ItemInfo> Item { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            ItemInfo i = new ItemInfo();
            i.ItemCode = context.GetValue<string>(this.ItemCode);

            switch (i.ItemCode)
            {
              case "12345":
                i.Description = "Widget";
                i.Price = (decimal)10.0;
                break;

                case "12346":
                i.Description = "Gadget";
                i.Price = (decimal)15.0;
                break;

                case "12347":
                i.Description = "Super Gadget";
                i.Price = (decimal)25.0;
                break;

                   
            }

            context.SetValue(this.Item, i);

        }
    }
}
