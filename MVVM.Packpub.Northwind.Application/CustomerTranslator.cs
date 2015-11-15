using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service =MVVM.Packpub.Northwind.Application.CustomerService;


namespace MVVM.Packpub.Northwind.Application
{
    class CustomerTranslator : IEntityTranslator<Model.Customer, Service.Customer>
    {
        internal static IEntityTranslator<Model.Customer,
           Service.Customer> _instance;

        public static IEntityTranslator<Model.Customer,
            Service.Customer> Instance
        {
            get
            {
                return _instance ??
                       (_instance = new CustomerTranslator());
            }
        }

        public Model.Customer CreateModel(
            CustomerService.Customer dto)
        {
            return UpdateModel(new Model.Customer(), dto);
        }

        public Model.Customer UpdateModel(Model.Customer model,
                                          CustomerService.
                                              Customer dto)
        {
            if (model.CustomerID != dto.CustomerID)
                model.CustomerID = dto.CustomerID;
            if (model.CompanyName != dto.CompanyName)
                model.CompanyName = dto.CompanyName;
            if (model.ContactName != dto.ContactName)
                model.ContactName = dto.ContactName;
            if (model.Address != dto.Address)
                model.Address = dto.Address;
            if (model.City != dto.City)
                model.City = dto.City;
            if (model.Region != dto.Region)
                model.Region = dto.Region;
            if (model.Country != dto.Country)
                model.Country = dto.Country;
            if (model.PostalCode != dto.PostalCode)
                model.PostalCode = dto.PostalCode;
            if (model.Phone != dto.Phone)
                model.Phone = dto.Phone;
            if (dto.Orders != null)
            {
                model.Orders = GetOrdersFromDto(dto);
            }

            return model;
        }

        private static ObservableCollection<MVVM.Packpub.Northwind.Model.Order> GetOrdersFromDto(MVVM.Packpub.Northwind.Application.CustomerService.Customer dto)
        {
            IEnumerable<MVVM.Packpub.Northwind.Model.Order> orders
                = dto.Orders.Select(o => new Model.Order
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    OrderDetails = GetOrderDetailsFromDto(o),
                    Freight = o.Freight,
                    ShippedDate = o.ShippedDate
                });
            return new ObservableCollection<MVVM.Packpub.Northwind.Model.Order>(orders);
        }

        private static IEnumerable<Model.OrderDetails> GetOrderDetailsFromDto(CustomerService.Order order)
        {
            return order.OrderDetails.Select(
                od => new Model.OrderDetails
                {
                    Product = GetProductFromDto(od),
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                });
        }

        private static MVVM.Packpub.Northwind.Model.Product GetProductFromDto(CustomerService.OrderDetails od)
        {
            return new MVVM.Packpub.Northwind.Model.Product
                       {
                           ProductID = od.Product.ProductID,
                           ProductName = od.Product.ProductName
                       };
        }

        public CustomerService.Customer CreateDto(Model.Customer model)
        {
            return UpdateDto(new Service.Customer(), model);
        }

        public CustomerService.Customer UpdateDto(
            CustomerService.Customer dto, Model.Customer model)
        {
            if (dto.CustomerID != model.CustomerID)
                dto.CustomerID = model.CustomerID;
            if (dto.CompanyName != model.CompanyName)
                dto.CompanyName = model.CompanyName;
            if (dto.ContactName != model.ContactName)
                dto.ContactName = model.ContactName;
            if (dto.Address != model.Address)
                dto.Address = model.Address;
            if (dto.Region != model.Region)
                dto.Region = model.Region;
            if (dto.Country != model.Country)
                dto.Country = model.Country;
            if (dto.PostalCode != model.PostalCode)
                dto.PostalCode = model.PostalCode;
            return dto;
        }
    }
}
