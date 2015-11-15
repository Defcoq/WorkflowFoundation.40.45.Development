namespace MVVM.Packpub.OrderEntry.UI.WPF.Model
{
	using System.Collections.Generic;
	using System.Activities;
    using MVVM.Packpub.OrderEntry.Workflow.BusinessRulesWF;
	using System.Threading;
	using System.ComponentModel;
    using MVVM.Packpub.OrderEntry.Workflow.Interface;

	class OrderModel : IOrder, INotifyPropertyChanged
	{
		public string CustomerName { get; set; }
		public string ProductName { get; set; }
		public string ProductQuantity { get; set; }

		string _validationSummary;
		public string ValidationSummary
		{
			get { return _validationSummary; }
			set
			{
				_validationSummary = value;
				RaisePropertyChanged("ValidationSummary");

			}
		}

		public void Validate()
		{
			Dictionary<string, object> arguments =
				new Dictionary<string, object>() 
		{ 
			{ "Order", this } 
		};

			WorkflowApplication workflowApp =
				new WorkflowApplication(new /*OrderRuleService()*/ SlowOrderRuleService(), arguments)
				{
					Completed = (e) =>
					{
						ValidationSummary = e.Outputs["ValidationSummary"].ToString();
					}
				};

			workflowApp.Run();
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged
			= delegate { };

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged(this,
				new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}
}
