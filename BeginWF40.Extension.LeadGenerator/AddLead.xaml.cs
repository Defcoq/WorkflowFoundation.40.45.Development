using LeadGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Configuration;
using System.Runtime.DurableInstancing;
using System.Activities.DurableInstancing;
using System.Activities;
using System.Data.Linq;
using LeadGenerator.Extensions;
using System.Activities.Tracking;

namespace BeginWF40.Extension.LeadGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddLead : Window
    {

        private string _connectionString = "";
        private InstanceStore _instanceStore;
        private DBExtension _dbExtension;
        private ListBoxTrackingParticipant _tracking;
        private EtwTrackingParticipant _etwTracking;
        private SqlTrackingParticipant _sqlTracking;





        public AddLead()
        {
            InitializeComponent();
            ApplicationInterface._app = this;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Open the config file and get the connection string


            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection css = (ConnectionStringsSection)config.GetSection("connectionStrings");
            _connectionString = css.ConnectionStrings["LeadGenerator"].ConnectionString;

            _dbExtension = new DBExtension(_connectionString);
            _instanceStore = new SqlWorkflowInstanceStore(_connectionString);
            InstanceView view = _instanceStore.Execute(_instanceStore.CreateInstanceHandle(), new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
            _instanceStore.DefaultInstanceOwner = view.InstanceOwner;

            
            // Set up the tracking participants
            CreateTrackingParticipant();
            CreateETWTrackingParticipant();
            CreateSqlTrackingParticipant();

            LoadExistingLeads();



        }


        private void CreateSqlTrackingParticipant()
        {
            _sqlTracking = new SqlTrackingParticipant(_connectionString)
            {
                TrackingProfile = new TrackingProfile()
                {
                    Name = "SqlTrackingProfile",
                    Queries = 
                    {
                        new WorkflowInstanceQuery()
                        {
                            States = { "*" },
                        },
                        
                        new BookmarkResumptionQuery()
                        {
                            Name = "*"
                        },
                        new ActivityStateQuery()
                        {
                            // Subscribe for track records from all activities 
                            // for all states
                            ActivityName = "*",
                            States = { "*" },
                        },   
                        // For User data, track all events
                        new CustomTrackingQuery() 
                        {
         
                            Name = "*",
                            ActivityName = "*"
                        }
                    }
                }
            };
        }

        private void CreateETWTrackingParticipant()
        {
            _etwTracking = new EtwTrackingParticipant()
            {
                TrackingProfile = new TrackingProfile()
                {
                    Name = "EtwTrackingProfile",
                    Queries =
                            {
                            new CustomTrackingQuery()
                            {
                            Name = "*",
                            ActivityName = "*"
                            }
                            }
                }
            };
        }


        private void CreateTrackingParticipant()
        {
            _tracking = new ListBoxTrackingParticipant(this.lstEvents)
            {
                TrackingProfile = new TrackingProfile()
                    {
                        Name = "ListBoxTrackingProfile",
                        Queries =
                        {
                            // For instance data, only track the started and completed events
                            new WorkflowInstanceQuery()
                            {
                            States = { WorkflowInstanceStates.Started, WorkflowInstanceStates.Completed },
                            },
                            // For bookmark data, only track the GetAssignment event
                            new BookmarkResumptionQuery()
                            {
                            Name = "GetAssignment"
                            },
                            // For activity data, track all states of the InvokeMethod
                            new ActivityStateQuery()
                            {
                            ActivityName = "InvokeMethod",
                            States = { "*" },
                            },
                            // For User data, track all events
                            new CustomTrackingQuery()
                            {
                            Name = "*",
                            ActivityName = "*"
                            }

                        }

                    }
            };
        }

        private void AddEvent(string szText)
        {
            lstEvents.Items.Add(szText);
        }
        public ListBox GetEventListBox()
        {
            return this.lstEvents;
        }

        public void AddNewLead(Lead l)
        {
            this.lstLeads.Dispatcher.BeginInvoke
                (new Action(() => this.lstLeads.Items.Add(l)));
        }

        private void btnAddLead_Click(object sender, RoutedEventArgs e)
        {
            // Setup a dictionary object for passing parameters
            Dictionary<string, object> parameters =
            new Dictionary<string, object>();
            parameters.Add("ContactName", txtName.Text);
            parameters.Add("ContactPhone", txtPhone.Text);
            parameters.Add("Interests", txtInterest.Text);
            parameters.Add("Notes", txtNotes.Text);
            // parameters.Add("ConnectionString", _connectionString);
            parameters.Add("Rating", int.Parse(txtRating.Text));
            parameters.Add("Writer", new ListBoxTextWriter(lstEvents));
            WorkflowApplication i = new WorkflowApplication(new EnterLead(), parameters);
            // Setup persistence
            // i.InstanceStore = _instanceStore;
            // i.PersistableIdle = (waiea) => PersistableIdleAction.Unload;
            SetupInstance(i);

            i.Run();


        }

        private void lstLeads_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (lstLeads.SelectedIndex >= 0)
            {
                Lead l = (Lead)lstLeads.Items[lstLeads.SelectedIndex];
                UpdateControls(l);
            }
            else
            {
                lblSelectedNotes.Content = "";
                lblSelectedNotes.Visibility = Visibility.Hidden;
                lblAgent.Visibility = Visibility.Hidden;
                txtAgent.Visibility = Visibility.Hidden;
                btnAssign.Visibility = Visibility.Hidden;
            }

        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (lstLeads.SelectedIndex >= 0)
            {
                Lead l = (Lead)lstLeads.Items[lstLeads.SelectedIndex];
                Guid id = l.WorkflowID;
                WorkflowApplication i = new WorkflowApplication(new EnterLead());
                SetupInstance(i);
                i.Load(id);
                try
                {
                    i.ResumeBookmark("GetAssignment", txtAgent.Text);
                }
                catch (Exception e2)
                {
                    AddEvent(e2.Message);
                }
            }

            //if (lstLeads.SelectedIndex >= 0)
            //{
            //    Lead l = (Lead)lstLeads.Items[lstLeads.SelectedIndex];
            //    Guid id = l.WorkflowID;

            //    LeadDataDataContext dc = new LeadDataDataContext(_connectionString);
            //    dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Leads);
            //    l = dc.Leads.SingleOrDefault<Lead>(x => x.WorkflowID == id);
            //    if (l != null)
            //    {
            //        l.AssignedTo = txtAgent.Text;
            //        l.Status = "Assigned";
            //        dc.SubmitChanges();

            //        // Clear the input
            //        txtAgent.Text = "";
            //    }

            //    // Update the grid
            //    lstLeads.Items[lstLeads.SelectedIndex] = l;
            //    lstLeads.Items.Refresh();

            //    WorkflowApplication i = new WorkflowApplication(new EnterLead());
            //    //i.InstanceStore = _instanceStore;
            //    // i.PersistableIdle = (waiea) => PersistableIdleAction.Unload;
            //    SetupInstance(i);

            //    i.Load(id);

            //    try
            //    {
            //        i.ResumeBookmark("GetAssignment", l);
            //    }
            //    catch (Exception e2)
            //    {
            //        AddEvent(e2.Message);
            //    }
            //}
        }

        private void LoadExistingLeads()
        {
            LeadDataDataContext dc = new LeadDataDataContext(_connectionString);
            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Leads);
            IEnumerable<Lead> q = dc.Leads
                .Where<Lead>(x => x.Status == "Open" || x.Status == "Assigned");
            foreach (Lead l in q)
            {
                AddNewLead(l);
            }
        }


        private void SetupInstance(WorkflowApplication i)
        {
            // Setup the instance store
            i.InstanceStore = _instanceStore;
            // Setup the PersistableIdle event handler
            i.PersistableIdle = (waiea) => PersistableIdleAction.Unload;
            i.Extensions.Add(_dbExtension);
            // Set up tracking
            i.Extensions.Add(_tracking);
            i.Extensions.Add(_etwTracking);
            i.Extensions.Add(_sqlTracking);
            // Setup persistence
            i.Extensions.Add(new PersistLead(_connectionString));
            i.Extensions.Add(new PersistAssignment(_connectionString));
           // i.Extensions.Add(new PersistAssignment(_connectionString));



            i.Completed = (wacea) =>
            {
                // Get the CommentExtension
                IEnumerable<CommentExtension> q = wacea.GetInstanceExtensions<CommentExtension>();
                // Add the comments to the event log
                if (q.Count() > 0)
                {
                    string comments = "Comments: \r\n" +
                    q.First<CommentExtension>().Comments;
                    this.lstEvents.Dispatcher.BeginInvoke
                    (new Action(() => this.lstEvents.Items.Add(comments)));
                }
                this.lstEvents.Dispatcher.BeginInvoke
                (new Action(() => this.lstEvents.Items.Add("\r\nThe workflow has completed")));
            };

            // Display any unhandled exceptions
            i.OnUnhandledException = (waueea) =>
            {
                this.lstEvents.Dispatcher.BeginInvoke(new Action(() =>this.lstEvents.Items.Add(waueea.UnhandledException.Message)));
                return UnhandledExceptionAction.Terminate;
            };
            // Display an error when an instance is aborted
            i.Aborted = (waaea) =>
            {
                this.lstEvents.Dispatcher.BeginInvoke(new Action(() =>this.lstEvents.Items.Add("Aborted: " + waaea.Reason.Message + "\n" +
                waaea.Reason.InnerException.Message)));
            };


        }


        private void UpdateControls(Lead l)
        {
            lblSelectedNotes.Content = l.Comments;
            lblSelectedNotes.Visibility = Visibility.Visible;
            if (l.Status == "Open")
            {
                lblAgent.Visibility = Visibility.Visible;
                txtAgent.Visibility = Visibility.Visible;
                btnAssign.Visibility = Visibility.Visible;
            }
            else
            {
                lblAgent.Visibility = Visibility.Hidden;
                txtAgent.Visibility = Visibility.Hidden;
                btnAssign.Visibility = Visibility.Hidden;
            }
        }

        public void UpdateLead(Lead lead)
        {
            // Find the row that matches this record
            int nSelected = -1;
            for (int i = 0; i < lstLeads.Items.Count; i++)
            {
                Lead l = lstLeads.Items[i] as Lead;
                if (l.LeadID == lead.LeadID)
                {
                    nSelected = i;
                    break;
                }
            }
            // Update the grid
            if (nSelected >= 0)
            {
                lstLeads.Items[nSelected] = lead;
                lstLeads.Items.Refresh();
                UpdateControls(lead);
            }
        }



    }
}
