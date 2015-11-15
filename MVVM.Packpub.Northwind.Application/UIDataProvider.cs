//using MVVM.Packpub.Northwind.Data;
using MVVM.Packpub.Northwind.Application;
using MVVM.Packpub.Northwind.Application.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Application
{
   public  class UIDataProvider : IUIDataProvider
    {
        private IList<Model.Customer> _customers;

        private readonly ICustomerService
            _customerServiceClient;

        public UIDataProvider(ICustomerService customerService)
        {
            _customerServiceClient = customerService;
        }

        public IList<Model.Customer> GetCustomers()
        {
            return _customers ??
                   (_customers =
                    _customerServiceClient.GetCustomers()
                        .Select(
                            c =>
                            CustomerTranslator.Instance.
                                CreateModel(c))
                        .ToList());
        }

        public Model.Customer GetCustomer(string customerID)
        {
            return
                CustomerTranslator.Instance.UpdateModel(
                    _customers
                        .First(c => c.CustomerID == customerID),
                    _customerServiceClient
                        .GetCustomer(customerID));
        }

        public void Update(Model.Customer customer)
        {
            _customerServiceClient.Update(
                CustomerTranslator.Instance.CreateDto(customer));
        }
   }

}
