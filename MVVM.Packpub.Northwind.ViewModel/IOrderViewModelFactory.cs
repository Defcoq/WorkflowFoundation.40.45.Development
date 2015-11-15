using MVVM.Packpub.Northwind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public interface IOrderViewModelFactory
    {
        OrderViewModel CreateInstance(Order order, Customer customer);

    }
}
