//MVVM.Packpub.Northwind.Data;
//using MVVM.Packpub.Northwind.Application;
//using MVVM.Packpub.Northwind.Application.CustomerService;
using MVVM.Packpub.Northwind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Application
{
    public interface IUIDataProvider
    {
        IList<Customer> GetCustomers();
        Customer GetCustomer(string customerID);
        void Update(Customer customer);


    }

}
