using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MVVM.Packpub.Northwind.Application.CustomerService;
using MVVM.Packpub.Northwind.Model;
using MVVM.Packpub.Northwind.Application;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
//using System.Data.Objects;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class MainWindowViewModel
    {

        private readonly IUIDataProvider _dataProvider;
        private readonly IToolManager _toolManager;

        private RelayCommand _showDetailsCommand;
        public RelayCommand ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand ??
                       (_showDetailsCommand =
                        new RelayCommand(
                            ShowCustomerDetails,
                            IsCustomerSelected));
            }
        }

        private string _selectedCustomerID;
        public string SelectedCustomerID
        {
            get { return _selectedCustomerID; }
            set
            {
                _selectedCustomerID = value;
                ShowDetailsCommand.RaiseCanExecuteChanged();
            }
        }

        private IList<Customer> _customers;
        public IList<Customer> Customers
        {
            get
            {
                if (_customers == null)
                {
                    GetCustomers();
                }
                return _customers;
            }
        }

        public ObservableCollection<ToolViewModel> Tools
        { get { return _toolManager.Tools; } }

        public MainWindowViewModel(
            IUIDataProvider dataProvider,
            IToolManager toolManager)
        {
            _dataProvider = dataProvider;
            _toolManager = toolManager;
        }

        public void ShowCustomerDetails()
        {
            if (!IsCustomerSelected())
                throw new InvalidOperationException(
                    "Unable to show customer because no "
                    + "customer is selected.");

            _toolManager.OpenTool(c => c.Customer.CustomerID == SelectedCustomerID,
                        () => new CustomerDetailsViewModel(
                        _dataProvider, SelectedCustomerID,
                        _toolManager));
                        
           // _toolManager.OpenCustomerDetails(SelectedCustomerID);
        }

        public bool IsCustomerSelected()
        {
            return !string.IsNullOrEmpty(SelectedCustomerID);
        }

        private void GetCustomers()
        {
            _customers =
                _dataProvider.GetCustomers();
        }
        //private readonly IUIDataProvider _dataProvider;

        //private readonly IToolManager _toolManager;

        //public ObservableCollection<ToolViewModel> Tools
        //{ get { return _toolManager.Tools; } }

        //public MainWindowViewModel(IUIDataProvider dataProvider, IToolManager toolManager)
        //{
        //    _dataProvider = dataProvider;
        //    _toolManager = toolManager;
        //}

        //public void ShowCustomerDetails()
        //            {
        //            if (!IsCustomerSelected())
        //            throw new InvalidOperationException("Unable to show customer because no " + "customer is selected.");
        //            _toolManager.OpenCustomerDetails(SelectedCustomerID);

        //            //_toolManager.OpenTool(c => c.Customer.CustomerID== SelectedCustomerID,
        //            //() => new CustomerDetailsViewModel(
        //            //_dataProvider, SelectedCustomerID,
        //            //_toolManager));
        //            }



        //private RelayCommand _showDetailsCommand;
        //public RelayCommand ShowDetailsCommand
        //{
        //    get
        //    {
        //        return _showDetailsCommand ??
        //               (_showDetailsCommand =
        //                new RelayCommand(
        //                    ShowCustomerDetails,
        //                    IsCustomerSelected));
        //    }
        //}

        //private string _selectedCustomerID;
        //public string SelectedCustomerID
        //{
        //    get { return _selectedCustomerID; }
        //    set
        //    {
        //        _selectedCustomerID = value;
        //        ShowDetailsCommand.RaiseCanExecuteChanged();
        //    }
        //}

        //private IList<Customer> _customers;
        //public IList<Customer> Customers
        //{
        //    get
        //    {
        //        if (_customers == null)
        //        {
        //            GetCustomers();
        //        }
        //        return _customers;
        //    }
        //}

        ////public ObservableCollection<ToolViewModel>
        ////    Tools { get; set; }

        ////public MainWindowViewModel(
        ////    IUIDataProvider dataProvider)
        ////{
        ////    _dataProvider = dataProvider;
        ////    Tools = new ObservableCollection<ToolViewModel>();
        ////}

        ////public void ShowCustomerDetails(/*object param*/)
        ////{
        ////    if (!IsCustomerSelected(/*param*/))
        ////        throw new InvalidOperationException(
        ////            "Unable to show customer because no "
        ////            + "customer is selected.");

        ////    CustomerDetailsViewModel customerDetailsViewModel
        ////        = GetCustomerDetailsTool(SelectedCustomerID);
        ////    if (customerDetailsViewModel == null)
        ////    {
        ////        customerDetailsViewModel
        ////            = new CustomerDetailsViewModel(
        ////                _dataProvider, SelectedCustomerID);
        ////        Tools.Add(customerDetailsViewModel);
        ////    }
        ////    SetCurrentTool(customerDetailsViewModel);
        ////}

        //public bool IsCustomerSelected(/*object param*/)
        //{
        //    return !string.IsNullOrEmpty(SelectedCustomerID);
        //}

        ////private CustomerDetailsViewModel GetCustomerDetailsTool(
        ////    string customerID)
        ////{
        ////    return Tools
        ////        .OfType<CustomerDetailsViewModel>()
        ////        .FirstOrDefault(c =>
        ////                        c.Customer.CustomerID ==
        ////                        customerID);
        ////}

        ////private void SetCurrentTool(ToolViewModel currentTool)
        ////{
        ////    ICollectionView collectionView =
        ////        CollectionViewSource.GetDefaultView(Tools);
        ////    if (collectionView != null)
        ////    {
        ////        if (collectionView.MoveCurrentTo(currentTool) !=
        ////            true)
        ////        {
        ////            throw new InvalidOperationException(
        ////                "Could not find the current tool.");
        ////        }
        ////    }
        ////}

        //private void GetCustomers()
        //{
        //    _customers =
        //        _dataProvider.GetCustomers();
        //}
       

    }
}
