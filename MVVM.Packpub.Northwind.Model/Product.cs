using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Model
{
    public class Product : ModelBase
    {
        public const string ProductIDPropertyName = "ProductID";
        private int _productID;
        public int ProductID
        {
            get { return _productID; }
            set
            {
                if (_productID == value)
                    return;
                _productID = value;
                RaisePropertyChanged(ProductIDPropertyName);
            }
        }

        public const string ProductNamePropertyName = "ProductName";
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (_productName == value)
                    return;
                _productName = value;
                RaisePropertyChanged(ProductNamePropertyName);
            }
        }

    }
}
