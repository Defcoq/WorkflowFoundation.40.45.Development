using MVVM.Packpub.Northwind.Application;
//using MVVM.Packpub.Northwind.Application.CustomerService;
using MVVM.Packpub.Northwind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;


namespace MVVM.Packpub.Northwind.ViewModel
{
    public class CustomerDetailsViewModel : ToolViewModel
    {

        private readonly IUIDataProvider _dataProvider;
        private readonly IOrdersViewModelFactory
            _ordersViewModelFactory;
        private bool _isDirty;

        public Customer Customer { get; set; }

        private RelayCommand _updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ??
                       (_updateCommand =
                        new RelayCommand(
                            UpdateCustomer,
                            CanUpdateCustomer));
            }
        }

        private OrdersViewModel _orders;
        public OrdersViewModel Orders
        {
            get
            {
                if (Customer == null)
                    return null;
                return _orders ?? (_orders
                = new OrdersViewModel(Customer.Orders));
            }
        }

        #region Originale OrdersViewModel
        //private OrdersViewModel _orders;
        //public OrdersViewModel Orders
        //{
        //    get
        //    {
        //        if (Customer == null)
        //            return null;
        //        return _orders
        //            ?? (_orders = _ordersViewModelFactory
        //                .CreateInstance(Customer));
        //    }
        //}
        #endregion

        private bool CanUpdateCustomer()
        {
            return _isDirty;
        }

        private void UpdateCustomer()
        {
            _dataProvider.Update(Customer);
        }

        public CustomerDetailsViewModel(
         IUIDataProvider dataProvider,
         string customerID,
         IToolManager toolManager = null
         )
            : base(toolManager)
        {
            _dataProvider = dataProvider;
            
            Customer = _dataProvider.GetCustomer(customerID);
            Customer.PropertyChanged
                += Customer_PropertyChanged;
            DisplayName = Customer.CompanyName;
        }

        public CustomerDetailsViewModel(
            IUIDataProvider dataProvider,
            string customerID,
            IToolManager toolManager,
            IOrdersViewModelFactory ordersViewModelFactory)
            : base(toolManager)
        {
            _dataProvider = dataProvider;
            _ordersViewModelFactory = ordersViewModelFactory;
            Customer = _dataProvider.GetCustomer(customerID);
            Customer.PropertyChanged
                += Customer_PropertyChanged;
            DisplayName = Customer.CompanyName;
        }

        void Customer_PropertyChanged(object sender,
            PropertyChangedEventArgs e)
        {
            _isDirty = true;
            UpdateCommand.RaiseCanExecuteChanged();
        }
        //private readonly IUIDataProvider _dataProvider;
        //private readonly IToolManager _toolManager;
        //private readonly IOrdersViewModelFactory _ordersViewModelFactory;


        //public Customer Customer { get; set; }
        //private bool _isDirty;
        //private RelayCommand _updateCommand;
        //public RelayCommand UpdateCommand
        //{
        //    get
        //    {
        //        return _updateCommand ??
        //        (_updateCommand =
        //        new RelayCommand(
        //        UpdateCustomer,
        //        CanUpdateCustomer));
        //    }
        //}
        //private bool CanUpdateCustomer()
        //{
        //    return _isDirty;
        //}
        //private void UpdateCustomer()
        //{
        //    _dataProvider.Update(Customer);
        //}

        //public CustomerDetailsViewModel(IUIDataProvider dataProvider, string customerID, IToolManager toolManager, IOrdersViewModelFactory ordersViewModelFactory)
        //    : base(toolManager)
        //{
        //    _dataProvider = dataProvider;
        //    _ordersViewModelFactory = ordersViewModelFactory;
        //    Customer = _dataProvider.GetCustomer(customerID);
        //    Customer.PropertyChanged += Customer_PropertyChanged;
        //    DisplayName = Customer.CompanyName;
           


        //}

        //void Customer_PropertyChanged(object sender,PropertyChangedEventArgs e)
        //{
        //    _isDirty = true;
        //    UpdateCommand.RaiseCanExecuteChanged();
        //}

        //private OrdersViewModel _orders;
        //public OrdersViewModel Orders
        //{
        //    get
        //    {
        //        if (Customer == null)
        //            return null;
        //        return _orders
        //            ?? (_orders
        //                = _ordersViewModelFactory
        //                .CreateInstance(Customer));
        //    }
        //}



    }
}
