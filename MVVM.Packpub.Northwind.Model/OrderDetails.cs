using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Model
{
   public class OrderDetails : ModelBase
    {
        public const string ProductPropertyName = "Product";
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                if (_product == value)
                    return;
                _product = value;
                RaisePropertyChanged(ProductPropertyName);
            }
        }

        public const string QuantityPropertyName = "Quantity";
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value)
                    return;
                _quantity = value;
                RaisePropertyChanged(QuantityPropertyName);
            }
        }

        public const string UnitPricePropertyName = "UnitPrice";
        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice == value)
                    return;
                _unitPrice = value;
                RaisePropertyChanged(UnitPricePropertyName);
            }
        }
    }
}
