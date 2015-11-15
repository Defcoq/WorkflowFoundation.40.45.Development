using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service = MVVM.Packpub.Northwind.Application.CustomerService;

namespace MVVM.Packpub.Northwind.Application
{
    public static class DataMapper
    {
        public static Model.Customer Update(this Model.Customer model, Service.Customer dto)
        {
            model.CustomerID = dto.CustomerID;
            model.CompanyName = dto.CompanyName;
            model.ContactName = dto.CompanyName;
            model.Address = dto.Address;
            model.Region = dto.Region;
            model.Country = dto.Region;
            model.PostalCode = dto.PostalCode;
            return model;
        }

    }
}
