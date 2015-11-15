using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public class ToolViewModel : ViewModelBase
    {
        private readonly IToolManager _toolManager;

        public string DisplayName { get; set; }

        private ICommand _closeCommand = null;
            public ICommand CloseCommand
            {
            get
                {
                    return _closeCommand ??
                    (_closeCommand =
                    new RelayCommand(Close));
                }
            }

            public ToolViewModel(IToolManager toolManager)
            {
                _toolManager = toolManager;
            }
            protected void Close()
            {
                _toolManager.CloseTool(this);
            }

    }

    //public class AToolViewModel : ToolViewModel
    //{
    //    public AToolViewModel()
    //    {
    //        DisplayName = "A";
    //    }
    //}

    //public class BToolViewModel : ToolViewModel
    //{
    //    public BToolViewModel()
    //    {
    //        DisplayName = "B";
    //    }
    //}

}
