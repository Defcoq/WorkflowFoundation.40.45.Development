using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Configuration.DSL;
using MVVM.Packpub.Northwind.Application.CustomerService;

namespace MVVM.Packpub.Northwind.Application
{
    public class RepositoryRegistry : Registry

    {
        public RepositoryRegistry()
        {
            For<IUIDataProvider>()
            .Singleton();
            For<ICustomerService>()
            .Singleton()
            .Use(() => new CustomerServiceClient());
        }

    }
}
