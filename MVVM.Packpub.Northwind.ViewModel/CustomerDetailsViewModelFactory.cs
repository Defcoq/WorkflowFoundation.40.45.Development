using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
   public class CustomerDetailsViewModelFactory : ICustomerDetailsViewModelFactory
    {
       private readonly IContainer _container;

       public CustomerDetailsViewModelFactory(IContainer container)
       {
           _container = container;

       }


       public CustomerDetailsViewModel CreateInstance(string customerID)
       {
           return _container
           .With("customerID")
           .EqualTo(customerID)
           .GetInstance<CustomerDetailsViewModel>();
       }

    }
}
