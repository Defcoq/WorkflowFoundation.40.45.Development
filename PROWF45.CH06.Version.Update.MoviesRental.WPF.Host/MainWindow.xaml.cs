using PROWF45.CH06.Verion.Update.MoviesRental.Activities.Model;
//using PROWF45.CH06.Version.Update.MoviesRental.Workflow;
using PROWF45.CH06.Verion.Update.MoviesRental.Activities;
using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROWF45.CH06.Version.Update.MoviesRental.WPF.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowApplication _wfApp;
        private SqlWorkflowInstanceStore _instanceStore;

        public MainWindow()
        {
            InitializeComponent();
            CreatePersistenceStore();
        }

        private void OnWorkflowIdle(WorkflowApplicationEventArgs args)
        {

        }
        private void OnWorkflowUnloaded(WorkflowApplicationEventArgs args)
        {

        }

        private void InitiateWorkflowRuntime()
        {
            try
            {
                Activity rentalWorkflow = new  MovieRentalProcess();
                _wfApp = new WorkflowApplication(rentalWorkflow,
                //    new WorkflowIdentity
                //{
                //    Name = "v1MovieRentalProcess",
                //    Version = new System.Version(1, 0, 0, 0)
                //});
                new WorkflowIdentity
                {
                    Name = "v2MovieRentalProcess",
                    Version = new System.Version(2, 0, 0, 0)
                });

                _wfApp.SynchronizationContext = SynchronizationContext.Current;
                _wfApp.OnUnhandledException = OnUnhandledException;
                _wfApp.Completed = OnWorkflowCompleted;
                _wfApp.Idle = OnWorkflowIdle;
                _wfApp.InstanceStore = _instanceStore;
                _wfApp.Unloaded = OnWorkflowUnloaded;

                //_wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e)
                //{
                //    // Instruct the runtime to persist and unload the workflow 
                //    return PersistableIdleAction.Persist;
                //};

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnWorkflowCompleted(WorkflowApplicationCompletedEventArgs wc)
        {
            if(wc.Outputs.ContainsKey("OutMovieRental"))
            {
                var createdRental = wc.Outputs["OutMovieRental"] as CustomerRental;
                MessageBox.Show(string.Format("New rental for {0} {1} has been created!", createdRental.PaymentCard.FirstName, createdRental.PaymentCard.LastName));
      
            }
        }

        private UnhandledExceptionAction OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs uh)
        {
            return UnhandledExceptionAction.Terminate;
        }

        private void cmdStartRentalProcess_Click(object sender, RoutedEventArgs e)
        {
            InitiateWorkflowRuntime();
            _wfApp.Run();

        }
        private void cmdSelectMovie_Click(object sender, RoutedEventArgs e)
        {
            var movie = new Movie
            {
                MovieName = txtMovieName.Text,
                Rating = "PG",
                Price = Convert.ToDecimal(4.50)
            };
            _wfApp.ResumeBookmark("SelectMovie", movie);
        }

        private void cmdSelectionComplete_Click(object sender, RoutedEventArgs e)
        {
            _wfApp.ResumeBookmark("FinishedSearching", true);
        }

        private void cmdInsertCard_Click(object sender, RoutedEventArgs e)
        {
            var creditCard = new CreditCard()
            {
                CCNumber = "1235626427465",
                FirstName = "Bayer",
                LastName = "White",
                ExpireMonth = 10,
                ExpireYear = 14
            };

            _wfApp.ResumeBookmark("ScanPaymentCard", creditCard);
        }


        private void cmdReturnMovie_Click(object sender, RoutedEventArgs e)
        {
            InitiateWorkflowRuntime();
            WorkflowApplicationInstance wfInstance =WorkflowApplication.GetInstance(new Guid(txtInstanceId.Text), _wfApp.InstanceStore);
           int minVer=  wfInstance.DefinitionIdentity.Version.Minor;
            _wfApp.Load(wfInstance);
            _wfApp.Run();

            var creditCard = new CreditCard()
            {
                CCNumber = "1235626427465",
                FirstName = "Bayer",
                LastName = "White",
                ExpireMonth = 10,
                ExpireYear = 14
            };

            _wfApp.ResumeBookmark("ReturnMovie", creditCard);
        }

        private void cmdUnload_Click(object sender, RoutedEventArgs e)
        {
            _wfApp.Unload();
        }
        private void CreatePersistenceStore()
        {
            try
            {
                _instanceStore = new SqlWorkflowInstanceStore();
                _instanceStore.ConnectionString =
                    @"Data Source=(LocalDB)\v11.0;Initial Catalog=WFPersist;Integrated Security=True";
                _instanceStore.InstanceCompletionAction = InstanceCompletionAction.DeleteNothing;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
