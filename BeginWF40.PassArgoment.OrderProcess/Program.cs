using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using OrderProcess;
using System.Collections;
using System.Collections.Generic;

namespace BeginWF40.PassArgoment.OrderProcess
{

    class Program
    {
        static void Main(string[] args)
        {
            Order myOrder = new Order
            {
                OrderID = 1,
                Description = "Need some stuff",
                ShippingMethod = "2ndDay",
                TotalWeight = 100
            };

            myOrder.Items.Add(new OrderItem
            {
                OrderItemID = 1,
                Quantity = 1,
                ItemCode = "12345",
                Description = "Widget"
            });
            myOrder.Items.Add(new OrderItem
            {
                OrderItemID = 2,
                Quantity = 3,
                ItemCode = "12346",
                Description = "Gadget"
            });
            myOrder.Items.Add(new OrderItem
            {
                OrderItemID = 1,
                Quantity = 1,
                ItemCode = "12345",
                Description = "Widget"
            });
            myOrder.Items.Add(new OrderItem
            {
                OrderItemID = 2,
                Quantity = 3,
                ItemCode = "12346",
                Description = "Gadget"
            });


            IDictionary<string, object> input = new Dictionary<string, object>
           {
            {"argOrderInfo", myOrder}
           };

            Activity workflow1 = new OrderWF();
            IDictionary<string, object> output =  WorkflowInvoker.Invoke(workflow1, input);

            // Get the TotalAmount returned by the workflow
            decimal total = (decimal)output["argTotalAmount"];
            Console.WriteLine("Workflow returned ${0} for my order total", total);
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();

        }
    }
}
