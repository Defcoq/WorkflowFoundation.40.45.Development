using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.OrderEntry.Workflow.Interface
{
    public interface IOrder
    {
        string CustomerName { get; set; }
        string ProductName { get; set; }
        string ProductQuantity { get; set; }
    }
}
