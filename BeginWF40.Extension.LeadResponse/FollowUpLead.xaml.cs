using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Runtime.DurableInstancing;
using System.Data.Linq;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Description;
using LeadGenerator;
using LeadGenerator.Extensions;

namespace LeadResponse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FollowUpLead : Window
    {
        private string _connectionString = "";
        private InstanceStore _instanceStore;
        private DBExtension _dbExtension;
        private System.ServiceModel.Activities.WorkflowServiceHost _wsh;

        public FollowUpLead()
        {
            InitializeComponent();

            LeadResponse.ApplicationInterface._app = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Open the config file and get the connection string
            Configuration config =
                ConfigurationManager.OpenExeConfiguration
                    (ConfigurationUserLevel.None);
            ConnectionStringsSection css =
                (ConnectionStringsSection)config.GetSection("connectionStrings");
            _connectionString =
                css.ConnectionStrings["LeadResponse"].ConnectionString;

            _instanceStore = new SqlWorkflowInstanceStore(_connectionString);
            InstanceView view = _instanceStore.Execute
                (_instanceStore.CreateInstanceHandle(),
                new CreateWorkflowOwnerCommand(),
                TimeSpan.FromSeconds(30));
            _instanceStore.DefaultInstanceOwner = view.InstanceOwner;

            // Create the DBExtension
            _dbExtension = new DBExtension(_connectionString);

            // Create a service to handle incoming requests
            LoadExistingLeads();
            SetupHost();

            
        }

        private void SetupHost()
        {
            WorkflowService service = new WorkflowService
            {
                Name = "LeadResponse",
                Body = new WorkAssignment(),
                Endpoints =
                {
                    new Endpoint
                    {
                        ServiceContractName="CreateAssignment",
                        AddressUri = new Uri("http://localhost/CreateAssignment"),
                        Binding = new BasicHttpBinding(),
                    }
                }
            };

            // Create a WorkflowServiceHost that listens for incoming messages
            _wsh = new System.ServiceModel.Activities.WorkflowServiceHost(service);

            SqlWorkflowInstanceStoreBehavior instanceStoreBehavior
                = new SqlWorkflowInstanceStoreBehavior(_connectionString);
            instanceStoreBehavior.InstanceCompletionAction
                = InstanceCompletionAction.DeleteAll;
            instanceStoreBehavior.InstanceLockedExceptionAction
                = InstanceLockedExceptionAction.AggressiveRetry;
            _wsh.Description.Behaviors.Add(instanceStoreBehavior);

            WorkflowIdleBehavior wib = new WorkflowIdleBehavior();
            wib.TimeToUnload = TimeSpan.FromMilliseconds(100);
            _wsh.Description.Behaviors.Add(wib);

            _wsh.Description.Behaviors.Add
                (new DBExtensionBehavior(_connectionString));
            _wsh.Description.Behaviors.Add
                (new PersistAssignmentBehavior(_connectionString));

            // Open the service so it will listen for messages
            _wsh.Open();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // Close the WorkflowServiceHost
            _wsh.Close();
        }

        private void LoadExistingLeads()
        {
            LeadDataDataContext dc = new LeadDataDataContext(_connectionString);
            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Assignments);
            IEnumerable<Assignment> q = dc.Assignments
                .Where<Assignment>(x => x.Status == "Assigned" ||
                                        x.Status == "Completed");
            foreach (Assignment a in q)
            {
                AddAssignment(a);
            }
        }

        public void AddAssignment(Assignment a)
        {
            LeadDataDataContext dc = new LeadDataDataContext(_connectionString);

            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Leads);
            Assignment aTmp = dc.Assignments
                .SingleOrDefault<Assignment>
                    (x => x.AssignmentID == a.AssignmentID);

            if (aTmp != null)
                this.lstLeads.Items.Add(aTmp);
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            if (lstLeads.SelectedIndex >= 0)
            {
                Assignment a
                    = lstLeads.Items[lstLeads.SelectedIndex] as Assignment;
                a.Remarks = txtRemarks.Text;
                Guid id = a.WorkflowID;

                // Reload the workflow instance
                WorkflowApplication i
                    = new WorkflowApplication(new WorkAssignment());

                SetupInstance(i);
                i.Load(id);

                // Resume the instance from the last bookmark
                try
                {
                    i.ResumeBookmark("GetCompletion", a);
                }
                catch (Exception e2)
                {
                    AddEvent(e2.Message);
                }
            }
        }

        private void lstLeads_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (lstLeads.SelectedIndex >= 0)
            {
                Assignment a
                    = lstLeads.Items[lstLeads.SelectedIndex] as Assignment;
                UpdateControls(a);
            }
            else
            {
                lblSelectedNotes.Content = "";
                lblSelectedNotes.Visibility = Visibility.Hidden;
                btnComplete.Visibility = Visibility.Hidden;
                txtRemarks.Visibility = Visibility.Hidden;
            }
        }

        public void UpdateAssignment(Assignment assignment)
        {
            // Find the row that matches this record
            int nSelected = -1;
            for (int i = 0; i < lstLeads.Items.Count; i++)
            {
                Assignment a = lstLeads.Items[i] as Assignment;
                if (a.AssignmentID == assignment.AssignmentID)
                {
                    nSelected = i;
                    break;
                }
            }

            // Update the grid
            if (nSelected >= 0)
            {
                lstLeads.Items[nSelected] = assignment;
                lstLeads.Items.Refresh();

                UpdateControls(assignment);
            }
        }

        private void UpdateControls(Assignment a)
        {
            lblSelectedNotes.Content = a.Lead.Comments;
            lblSelectedNotes.Visibility = Visibility.Visible;

            lblDateAssigned.Content = a.DateAssigned.ToShortDateString();

            if (a.DateDue.HasValue)
                lblDateDue.Content = a.DateDue.Value.ToShortDateString();
            else
                lblDateDue.Content = "";

            if (a.DateCompleted.HasValue)
                lblDateCompleted.Content
                    = a.DateCompleted.Value.ToShortDateString();
            else
                lblDateCompleted.Content = "";

            txtRemarks.Visibility = Visibility.Visible;
            txtRemarks.Text = a.Remarks;

            if (a.Status == "Assigned")
            {
                btnComplete.Visibility = Visibility.Visible;
                txtRemarks.IsReadOnly = false;
            }
            else
            {
                btnComplete.Visibility = Visibility.Hidden;
                txtRemarks.IsReadOnly = true;
            }
        }

        private void SetupInstance(WorkflowApplication i)
        {
            // Setup the instance store
            i.InstanceStore = _instanceStore;

            // Setup the PersistableIdle event handler
            i.PersistableIdle = (waiea) => PersistableIdleAction.Unload;

            // Setup the connection string
            i.Extensions.Add(_dbExtension);

            i.Aborted = (waaea) =>
            {
                this.lstEvents.Dispatcher.BeginInvoke
                    (new Action(() => this.lstEvents.Items.Add
                        ("Aborted: " + waaea.Reason.Message)));
            };

            i.OnUnhandledException = (waueea) =>
            {
                this.lstEvents.Dispatcher.BeginInvoke
                    (new Action(() => this.lstEvents.Items.Add
                        (waueea.UnhandledException.Message)));
                return UnhandledExceptionAction.Terminate;
            };

            // Display the accumulated comments
            i.Completed = (wacea) =>
            {
                this.lstEvents.Dispatcher.BeginInvoke
                    (new Action(() => this.lstEvents.Items.Add
                        ("\r\nThe workflow has completed")));
            };

            i.Unloaded = (waea) =>
            {
                this.lstEvents.Dispatcher.BeginInvoke
                    (new Action(() => this.lstEvents.Items.Add
                        ("Workflow unloaded")));
            };

            i.Extensions.Add(new PersistAssignment(_connectionString));
        }

        // Add a line of text to the Event Log
        private void AddEvent(string szText)
        {
            lstEvents.Items.Add(szText);
        }

        public ListBox GetEventListBox()
        {
            return this.lstEvents;
        }
    }
}
