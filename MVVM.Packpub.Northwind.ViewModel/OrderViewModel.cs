using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM.Packpub.Northwind.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        public Customer Customer { get; set; }
        private readonly IToolManager _toolManager;

        public const string ModelPropertyName = "Model";
        private Order _model;
        public Order Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;
                _model = value;
                RaisePropertyChanged(ModelPropertyName);
                RaisePropertyChanged(TotalPropertyName);
            }
        }

        public const string TotalPropertyName = "Total";
        public decimal Total
        {
            get
            {
                return _model.OrderDetails.Sum(o => o.Quantity + o.UnitPrice);
            }
        }

        public OrderViewModel(Order order, Customer customer, IToolManager toolManager)
        {
            Customer = customer;
            _model= order;
            _toolManager = toolManager;
            SubscribeToOrderDetailsChanged(_model);
        }

        public OrderViewModel(Order model)
        {
            _model = model;
            SubscribeToOrderDetailsChanged(_model);
        }

        private void SubscribeToOrderDetailsChanged(Order order)
        {
            order.PropertyChanged += Order_PropertyChanged;
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.PropertyChanged
                += Order_PropertyChanged;
            }
        }
        private void UnSubscribeToOrderDetailsChanged(Order order)
        {
            order.PropertyChanged -= Order_PropertyChanged;
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.PropertyChanged
                -= Order_PropertyChanged;
            }
        }
        void Order_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case Order.FreightPropertyName:
                case OrderDetails.QuantityPropertyName:
                case OrderDetails.UnitPricePropertyName:
                    RaisePropertyChanged(TotalPropertyName);
                    break;
            }
        }
        public override void Cleanup()
        {
            UnSubscribeToOrderDetailsChanged(Model);
            base.Cleanup();
        }

        public ICommand ShowOrderDetailsCommand
        {
            get
            {
                return new RelayCommand(
                    () => _toolManager.OpenOrderDetails(this));
            }
        }


        //public const string ModelPropertyName = "Model";
        //private Order _model;
        //public Customer Customer { get; set; }
        //private readonly IToolManager _toolManager;

        //public Order Model
        //{
        //    get { return _model; }
        //    set
        //    {
        //        if (_model == value)
        //            return;
        //        _model = value;
        //        RaisePropertyChanged(ModelPropertyName);
        //        RaisePropertyChanged(TotalPropertyName);
        //    }
        //}

        //public const string TotalPropertyName = "Total";
        //public decimal Total
        //{
        //    get
        //    {
        //        return _model.OrderDetails.Sum(
        //            o => o.Quantity + o.UnitPrice);
        //    }
        //}

        //public ICommand ShowOrderDetailsCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(
        //            () => _toolManager.OpenOrderDetails(this));
        //    }
        //}

        //public OrderViewModel(Order model,
        //    Customer customer, IToolManager toolManager)
        //{
        //    _model = model;
        //    Customer = customer;
        //    _toolManager = toolManager;
        //    SubscribeToOrderDetailsChanged(_model);
        //}

        //private void SubscribeToOrderDetailsChanged(
        //    Order order)
        //{
        //    order.PropertyChanged += Order_PropertyChanged;
        //    foreach (var orderDetail in order.OrderDetails)
        //    {
        //        orderDetail.PropertyChanged
        //            += Order_PropertyChanged;
        //    }
        //}

        //private void UnSubscribeToOrderDetailsChanged(
        //    Order order)
        //{
        //    foreach (var orderDetail in order.OrderDetails)
        //    {
        //        orderDetail.PropertyChanged
        //            -= Order_PropertyChanged;
        //    }
        //}

        //void Order_PropertyChanged(
        //    object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case Order.FreightPropertyName:
        //        case OrderDetail.QuantityPropertyName:
        //        case OrderDetail.UnitPricePropertyName:
        //            RaisePropertyChanged(TotalPropertyName);
        //            break;
        //    }
        //}

        //public override void Cleanup()
        //{
        //    UnSubscribeToOrderDetailsChanged(Model);
        //    base.Cleanup();
        //}
    }
}
