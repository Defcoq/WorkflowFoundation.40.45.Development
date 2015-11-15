using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Service
{
    [DataContract]
    public class OrderDetails
    {
        [DataMember]
        public Product Product { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal UnitPrice { get; set; }

    }
}
