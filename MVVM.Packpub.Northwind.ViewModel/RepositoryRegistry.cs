using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IToolManager>()
            .Singleton();
            For<ICustomerDetailsViewModelFactory>()
            .Singleton();
            For<IOrderViewModelFactory>()
            .Singleton();
            For<IOrdersViewModelFactory>()
            .Singleton();
        }

    }
}
