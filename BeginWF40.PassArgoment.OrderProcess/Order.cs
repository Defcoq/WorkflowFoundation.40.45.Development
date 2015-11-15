using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcess
{

    public static class OrderDiscount
    {
        public static decimal ComputeDiscount(Order o, decimal total)
        {
            // Count the number of items ordered
            int count = 0;
            foreach (OrderItem i in o.Items)
            {
                count += i.Quantity;
            }
            decimal pct = 0;
            if (total > 500)
                pct = (decimal)0.20;
            if (total > 200)
                pct = (decimal)0.15;
            if (total > 100)
                pct = (decimal)0.10;
            // Calculate the discount amount
            decimal discount = total * pct;
            // Subtract a dollar for every item ordered
            discount -= (decimal)count;
            // Make sure it’s not less than zero
            if (discount < 0)
                discount = 0;
            Console.WriteLine("Discount computed: ${0}", discount.ToString());
            return discount;
        }
    }

    public class ItemInfo
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }


    //---------------------------------------------
    // Define the exception to be thrown if an item
    // is out of stock
    //---------------------------------------------
    public class OutOfStockException : Exception
    {
        public OutOfStockException()
            : base()
        {
        }
        public OutOfStockException(string message)
            : base(message)
        {
        }
    }

    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
    }

    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public int OrderID { get; set; }
        public string Description { get; set; }
        public decimal TotalWeight { get; set; }
        public string ShippingMethod { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
