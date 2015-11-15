using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Service
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        IList<Customer> GetCustomers();
        [OperationContract]
        Customer GetCustomer(string customerID);
        [OperationContract]
        void Update(Customer customer);

    }

}
