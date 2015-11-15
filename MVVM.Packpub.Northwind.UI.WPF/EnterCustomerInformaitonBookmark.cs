using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace MVVM.Packpub.Northwind.UI.WPF
{

    public sealed class EnterCustomerInformaitonBookmark : NativeActivity<string>
    {
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark("EnterCustomerInformation");
        }

    }
}
