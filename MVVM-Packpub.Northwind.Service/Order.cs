using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Service
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public DateTime? OrderDate { get; set; }
        [DataMember]
        public DateTime? ShippedDate { get; set; }
        [DataMember]
        public decimal? Freight { get; set; }
        [DataMember]
        public IEnumerable<OrderDetails> OrderDetails { get; set; }

    }
}
