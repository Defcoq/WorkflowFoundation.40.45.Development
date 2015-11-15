using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class ToolManager : IToolManager
    {
        private readonly ICustomerDetailsViewModelFactory _customerDetailsFactory;
        private readonly IOrderDetailsViewModelFactory _orderDetailsFactory;


        private readonly ICollectionView _toolCollectionView;
        public ObservableCollection<ToolViewModel>
        Tools { get; set; }
        public ToolManager(ICustomerDetailsViewModelFactory customerDetailsFactory, IOrderDetailsViewModelFactory orderDetailsFactory
)
        {
            _customerDetailsFactory = customerDetailsFactory;
            _orderDetailsFactory = orderDetailsFactory;

            Tools = new ObservableCollection<ToolViewModel>();
            _toolCollectionView = CollectionViewSource.GetDefaultView(Tools);
        }

        public void OpenOrderDetails(OrderViewModel order)
        {
            OpenTool(p => p.Order.Model.OrderID == order.Model.OrderID,
            () => _orderDetailsFactory.CreateInstance(order));
        }


        public void OpenCustomerDetails(string customerId)
        {
            OpenTool(
            c => c.Customer.CustomerID == customerId,
            () => _customerDetailsFactory.CreateInstance(customerId));
        }


        public void OpenTool<T>(Func<T, bool> predicate,Func<T> toolFactory) where T : ToolViewModel
        {
            var tool = Tools
            .Where(t => t.GetType() == typeof(T))
            .FirstOrDefault(t => predicate.Invoke((T)t));
            if (tool == null)
            {
                tool = toolFactory.Invoke();
                Tools.Add(tool);
            }
            SetCurrentTool(tool);
        }
        public void CloseTool(ToolViewModel tool)
        {
            Tools.Remove(tool);
        }
        private void SetCurrentTool(ToolViewModel currentTool)
        {
            if (_toolCollectionView.MoveCurrentTo(currentTool)
            != true)
            {
                throw new InvalidOperationException(
                "Could not find the current tool.");
            }
        }

    }
}
