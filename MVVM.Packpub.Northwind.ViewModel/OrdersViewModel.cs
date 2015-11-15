using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
   public  class OrdersViewModel : ViewModelBase
    {
        public ObservableCollection<OrderViewModel> Orders { get; set; }

        public OrdersViewModel(IEnumerable<Model.Order> orders)
        {
            Orders = new ObservableCollection<OrderViewModel>(
            orders.Select(o => new OrderViewModel(o)));
        }

        public OrdersViewModel(Model.Customer model, IToolManager toolManager, IOrderViewModelFactory orderViewModelFactory)

        {
            Orders = new ObservableCollection<OrderViewModel>(model.Orders.Select(o => orderViewModelFactory.CreateInstance(o, model)));
        }

    }
}
