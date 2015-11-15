using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Packpub.Northwind.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace MVVM.Packpub.Northwind.Service
{
   public  class CustomerService : ICustomerService
    {
       private readonly MVVM.Packpub.Northwind.Data.NWindEntities _northwindEntities = new NWindEntities();

       public IList<Northwind.Service.Customer> GetCustomers()
       {
           return _northwindEntities.Customers
           .Select(
           c => new Northwind.Service.Customer
           {
               CustomerID = c.CustomerID,
               CompanyName = c.CompanyName
           }).ToList();
       }

       public Northwind.Service.Customer GetCustomer(string customerID)
       {
           Data.Customer customer= _northwindEntities.Customers.Single(c => c.CustomerID == customerID);
           return new Service.Customer
           {
               CustomerID = customer.CustomerID,
               CompanyName = customer.CompanyName,
               ContactName = customer.ContactName,
               Address = customer.Address,
               City = customer.City,
               Country = customer.Country,
               Region = customer.Region,
               PostalCode = customer.PostalCode,
               Phone = customer.Phone,
               Orders= _northwindEntities.Orders.Where(x=> x.CustomerID == customer.CustomerID).Select(
               o => new Service.Order
               {
                   OrderID = o.OrderID,
                   OrderDate = o.OrderDate,
                   OrderDetails =_northwindEntities.Order_Details.Where(x=> x.OrderID == o.OrderID).Select(
           od => new Service.OrderDetails
           {
           
               Product = new Service.Product
              {
                  ProductID
                  = od.ProductID,
                  ProductName = _northwindEntities.Products.FirstOrDefault(x=>x.ProductID == od.ProductID).ProductName
              },
               Quantity = od.Quantity,
               UnitPrice = od.UnitPrice
           }).ToList(),
                   Freight = o.Freight,
                   ShippedDate = o.ShippedDate
               }).ToList()
           };

       //    Northwind.Data.Customer customer
       //    = _northwindEntities
       //    .Customers.Single(
       //    c => c.CustomerID == customerID);
       //    return new Northwind.Service.Customer
       //    {
       //        CustomerID = customer.CustomerID,
       //        CompanyName = customer.CompanyName,
       //        ContactName = customer.ContactName,
       //        Address = customer.Address,
       //        City = customer.City,
       //        Country = customer.Country,
       //        Region = customer.Region,
       //        PostalCode = customer.PostalCode,
       //        Phone = customer.Phone
       //    };
       }

       //private static IEnumerable<Service.Order> GetOrders(string customerID)
       //{

       //    return order.Select(o => new Service.Order
       //    {
       //        OrderID = o.OrderID,
       //        OrderDate = o.OrderDate,
       //        OrderDetails = GetOrderDetails(o),
       //        Freight = o.Freight,
       //        ShippedDate = o.ShippedDate
       //    }).ToList();
       //}


       //private static IEnumerable<Service.OrderDetails> GetOrderDetails(Data.Order order)
       //{

       //    return Data.Order_Details.Select(
       //    o => new Service.OrderDetail
       //    {
       //        Product
       //        = new Service.Product
       //        {
       //            ProductID
       //            = o.Product.ProductID,
       //            ProductName
       //            = o.Product.ProductName
       //        },
       //        Quantity = o.Quantity,
       //        UnitPrice = o.UnitPrice
       //    }).ToList();
       //}



       public void Update(Customer customer)
       {
           Data.Customer customerEntity= _northwindEntities
           .Customers.First(
           c => c.CustomerID == customer.CustomerID);
           _northwindEntities.Customers.Attach(customerEntity);
        
           customerEntity.CompanyName = customer.CompanyName;
           customerEntity.ContactName = customer.ContactName;
           customerEntity.Address = customer.Address;
           customerEntity.City = customer.City;
           customerEntity.Country = customer.Country;
           customerEntity.Region = customer.Region;
           customerEntity.PostalCode = customer.PostalCode;
           customerEntity.Phone = customer.Phone;
           _northwindEntities.Entry(customerEntity).CurrentValues.SetValues(customerEntity);
           _northwindEntities.Entry(customerEntity).State = EntityState.Modified; 

           //Data.Customer customerEntityNew = new Data.Customer();
           //customerEntityNew.CompanyName = customer.CompanyName + "Prova JPP";
           //customerEntityNew.CustomerID = "PIPOO";
           //customerEntityNew.ContactName = customer.ContactName;
           //customerEntityNew.Address = customer.Address;
           //customerEntityNew.City = customer.City;
           //customerEntityNew.Country = customer.Country;
           //customerEntityNew.Region = customer.Region;
           //customerEntityNew.PostalCode = customer.PostalCode;
           //customerEntityNew.Phone = customer.Phone;
           //_northwindEntities.Customers.Add(customerEntityNew);
           //_northwindEntities.Entry(customerEntityNew).State = System.Data.Entity.EntityState.Added;
           _northwindEntities.SaveChanges();
       }


    }
}
