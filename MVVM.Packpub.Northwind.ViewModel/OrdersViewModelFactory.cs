using MVVM.Packpub.Northwind.Model;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
   public  class OrdersViewModelFactory : IOrdersViewModelFactory
    {
        private readonly IContainer _container; 
       public OrdersViewModelFactory (Container container)
        {
            _container = container;
        }
        public OrdersViewModel CreateInstance(Customer customer)
        {
            return _container
            .With("model")
            .EqualTo(customer)
            .GetInstance<OrdersViewModel>();
        }

    }
}
