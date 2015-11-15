using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using MVVM.Packpub.Northwind.Application.CustomerService;
using MVVM.Packpub.Northwind.Model;
using System.Collections.Generic;
using MVVM.Packpub.Northwind.Application;
using Rhino.Mocks;

namespace MVVM.Packpub.Northwind.ViewModel.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void Customers_Always_CallsGetCustomers()
        {
            // Create stub
            //IList<Customer> expected = GetCustomers();
            //UIDataProviderStub uiDataProviderStub = new UIDataProviderStub
            //{
            //    Customers = expected
            //};

            //IUIDataProvider uiDataProviderMock = MockRepository.GenerateMock<IUIDataProvider>();
            //uiDataProviderMock.Expect(c => c.GetCustomers());

            //// Inject stub
            //MainWindowViewModel target = new MainWindowViewModel(uiDataProviderMock);
            //IList<Customer> customers = target.Customers;
            //uiDataProviderMock.VerifyAllExpectations();

           // CollectionAssert.AreEquivalent((List<Customer>)expected,(List<Customer>)target.Customers);
        }

        //private IList<Customer> GetCustomers()
        //{
        //    const int numberOfCustomers = 10;
        //    IList<Customer> customers = new List<Customer>();
        //    for (int i = 0; i < numberOfCustomers; i++)
        //    {
        //        customers.Add(new Customer
        //        {
        //            CustomerID = "CustomerID " + i,
        //            CompanyName = "CompanyName " + i
        //        });
        //    }
        //    return customers;
        //}

       // [TestMethod]
       // public void Ctor_Always_CallsGetCustomer()
       // {
       //     // Arrange
       //     IUIDataProvider uiDataProviderMock= MockRepository.GenerateMock<IUIDataProvider>();
       //     const string expectedID = "EXPECTEDID";
       //     uiDataProviderMock.Expect(c => c.GetCustomer(expectedID)).Return(new Customer());
       //     // Act
       //     CustomerDetailsViewModel target= new CustomerDetailsViewModel(uiDataProviderMock, expectedID);
       //     // Assert
       //     uiDataProviderMock.VerifyAllExpectations();
       // }

       // [TestMethod]
       //public void Customer_Always_ReturnsCustomerFromGetCustomer()
       // {
       //     // Arrange
       //     IUIDataProvider uiDataProviderStub
       //     = MockRepository
       //     .GenerateStub<IUIDataProvider>();
       //     const string expectedID = "EXPECTEDID";
       //     Customer expectedCustomer
       //     = new Customer
       //     {
       //         CustomerID =
       //         expectedID
       //     };
       //     uiDataProviderStub.Stub(
       //     c => c.GetCustomer(expectedID)).Return(
       //     expectedCustomer);
       //     // Act
       //     CustomerDetailsViewModel target
       //     = new CustomerDetailsViewModel(
       //     uiDataProviderStub, expectedID);
       //     // Assert
       //     Assert.AreSame(expectedCustomer, target.Customer);


       // }

       // [TestMethod]
       // public void DisplayName_Always_ReturnsCompanyName()
       // {
       //     // Arrange
       //     IUIDataProvider uiDataProviderStub
       //     = MockRepository
       //     .GenerateStub<IUIDataProvider>();
       //     const string expectedID = "EXPECTEDID";
       //     const string expectedCompanyName = "EXPECTEDNAME";
       //     Customer expectedCustomer
       //     = new Customer
       //     {
       //         CustomerID =
       //         expectedID,
       //         CompanyName =
       //         expectedCompanyName
       //     };
       //     uiDataProviderStub.Stub(
       //     c => c.GetCustomer(expectedID)).Return(
       //     expectedCustomer);
       //     // Act
       //     CustomerDetailsViewModel target
       //     = new CustomerDetailsViewModel(
       //     uiDataProviderStub, expectedID);
       //     // Assert
       //     Assert.AreEqual(expectedCompanyName,
       //     target.DisplayName);
       // }

        //private class UIDataProviderStub : IUIDataProvider
        //{
        //    public IList<Customer> Customers { private get; set; }
        //    public IList<Customer> GetCustomers()
        //    {
        //        return Customers;
        //    }


        //    public Customer GetCustomer(string customerID)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    IList<Application.CustomerService.Customer> IUIDataProvider.GetCustomers()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    Application.CustomerService.Customer IUIDataProvider.GetCustomer(string customerID)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}


    }
}
