using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM.Packpub.OrderEntry.UI.WPF.Model;



namespace MVVM.Packpub.OrderEntry.UI.WPF.ViewModel
{
 
   public  class OrderViewModel : ViewModelBase
    {
        #region Constructors

        public OrderViewModel()
        {
            _model.PropertyChanged +=
                new System.ComponentModel
                    .PropertyChangedEventHandler(
                        _model_PropertyChanged);
        }


        #endregion Constructors

        #region Private Fields

        OrderModel _model = new OrderModel();

        #endregion

        #region Public Properties

        string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                RaisePropertyChanged("CustomerName");
            }
        }

        string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                RaisePropertyChanged("ProductName");
            }
        }

        string _productQuantity;
        public string ProductQuantity
        {
            get { return _productQuantity; }
            set
            {
                _productQuantity = value;
                RaisePropertyChanged("ProductQuantity");
            }
        }


        string _validationSummary = string.Empty;
        public string ValidationSummary
        {
            get { return _validationSummary; }
            set
            {
                _validationSummary = value;
                this.RaisePropertyChanged("ValidationSummary");
            }
        }

        bool _isValid = true;
        public bool IsValid
        {
            get { return _isValid; }
            private set
            {
                _isValid = value;
                RaisePropertyChanged("IsValid");
            }
        }

        #endregion

        #region Commands

        RelayCommand _submitOrderCommand;
        public RelayCommand SubmitOrderCommand
        {
            get
            {
                if (_submitOrderCommand == null)
                {
                    _submitOrderCommand = new RelayCommand(submitOrder);
                }

                return _submitOrderCommand;
            }
        }

        #endregion

        #region Private Methods

        private void submitOrder()
        {
            IsValid = true;

            _model.CustomerName = CustomerName;
            _model.ProductName = ProductName;
            _model.ProductQuantity = ProductQuantity;

            _model.Validate();

        }


        #endregion

        #region Event Handlers

        void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ValidationSummary")
            {
                ValidationSummary = _model.ValidationSummary;
                if (ValidationSummary.Length > 0)
                {
                    IsValid = false;
                }
            }
        }

        #endregion
    }
}
