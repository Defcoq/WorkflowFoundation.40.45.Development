using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class OrderDetailsViewModel : ToolViewModel
    {
        private readonly IToolManager _toolManager;
        public ICustomerDetailsViewModelFactory
        CustomerDetailsFactory { get; set; }
        public OrderViewModel Order { get; set; }

        public ICommand ShowCustomerDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                _toolManager.OpenCustomerDetails(
                Order.Customer.CustomerID));
            }

        }

        public OrderDetailsViewModel(OrderViewModel order,IToolManager toolManager)
            : base(toolManager)
        {
            _toolManager = toolManager;
            Order = order;
            DisplayName = Order.Customer.CompanyName
            + ": " + Order.Model.OrderID.ToString();
        }

    }
}
