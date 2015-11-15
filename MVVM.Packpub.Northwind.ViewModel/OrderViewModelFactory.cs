using MVVM.Packpub.Northwind.Model;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class OrderViewModelFactory : IOrderViewModelFactory
    {
        private readonly IContainer _container;

        public OrderViewModelFactory(
            IContainer container)
        {
            _container = container;
        }

        public OrderViewModel CreateInstance(Order order, Customer customer)
        {
            return _container
                .With("order")
                .EqualTo(order)
                .With("customer")
                .EqualTo(customer)
                .GetInstance<OrderViewModel>();
        }
    }
}
