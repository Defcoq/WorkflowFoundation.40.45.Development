using System;
using System.Activities.Presentation.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PROWF40.CH17.DesignerHost
{
   public  class ValidationErrorService  : IValidationErrorService

    {
        private ListBox lb;
        public ValidationErrorService(ListBox listBox)
        {
            lb = listBox;
        }
        public void ShowValidationErrors(IList<ValidationErrorInfo> errors)
        {
            lb.Items.Clear();
            foreach (ValidationErrorInfo error in errors)
            {
                if (String.IsNullOrEmpty(error.PropertyName))
                {
                    lb.Items.Add(error.Message);
                }
                else
                {
                    lb.Items.Add(String.Format("{0}: {1}",error.PropertyName,error.Message));
                }
            }
        }

    }
}
