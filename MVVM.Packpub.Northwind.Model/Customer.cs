using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Model
{
   public  class Customer : ModelBase
    {
        private string _customerID;
        public string CustomerID
        {
            get { return _customerID; }
            set
            {
                if (string.Compare(_customerID, value) == 0)
                    return;
                _customerID = value;
                RaisePropertyChanged("CustomerID");
            }
        }
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (string.Compare(_companyName, value) == 0)
                    return;
                _companyName = value;
                RaisePropertyChanged("CompanyName");
            }
        }

        private string _contactName;
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                if (string.Compare(_contactName, value) == 0)
                    return;
                _contactName = value;
                RaisePropertyChanged("ContactName");
            }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                if (string.Compare(_address, value) == 0)
                    return;
                _address = value;
                RaisePropertyChanged("Address");
            }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (string.Compare(_city, value) == 0)
                    return;
                _city = value;
                RaisePropertyChanged("City");
            }
        }
        private string _region;
        public string Region
        {
            get { return _region; }
            set
            {
                if (string.Compare(_region, value) == 0)
                    return;
                _region = value;
                RaisePropertyChanged("Region");
            }
        }
        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (string.Compare(_postalCode, value) == 0)
                    return;
                _postalCode = value;
                RaisePropertyChanged("PostalCode");
            }
        }
        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (string.Compare(_postalCode, value) == 0)
                    return;
                _country = value;
                RaisePropertyChanged("Country");
            }
        }
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (string.Compare(_phone, value) == 0)
                    return;
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }


        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                if (_orders == value)
                    return;
                _orders = value;
                RaisePropertyChanged("Orders");
            }
        }


    }
}
