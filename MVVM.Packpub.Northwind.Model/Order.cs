using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Model
{
   public  class Order : ModelBase
    {

        public const string OrderIDPropertyName = "OrderID";
        private int _orderID;
        public int OrderID
        {
            get { return _orderID; }
            set
            {
                if (_orderID == value)
                    return;
                _orderID = value;
                RaisePropertyChanged(OrderIDPropertyName);
            }
        }

        public const string OrderDatePropertyName = "OrderDate";
        private DateTime? _orderDate;
        public DateTime? OrderDate
        {
            get { return _orderDate; }
            set
            {
                if (_orderDate == value)
                    return;
                _orderDate = value;
                RaisePropertyChanged(OrderDatePropertyName);
            }
        }

        public const string ShippedDatePropertyName = "ShippedDate";
        private DateTime? _shippedDate;
        public DateTime? ShippedDate
        {
            get { return _shippedDate; }
            set
            {
                if (_shippedDate == value)
                    return;
                _shippedDate = value;
                RaisePropertyChanged(ShippedDatePropertyName);
            }
        }

        public const string FreightPropertyName = "Freight";
        private decimal? _freight;
        public decimal? Freight
        {
            get { return _freight; }
            set
            {
                if (_freight == value)
                    return;
                _freight = value;
                RaisePropertyChanged(FreightPropertyName);
            }
        }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
