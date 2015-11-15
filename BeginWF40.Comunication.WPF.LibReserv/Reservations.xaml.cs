using LibraryReservation;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
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

namespace BeginWF40.Comunication.WPF.LibReserv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost _sh;
        private IDictionary<Guid, WorkflowApplication> _incomingRequests;
        private IDictionary<Guid, WorkflowApplication> _outgoingRequests;


        public MainWindow()
        {
            InitializeComponent();
            ApplicationInterface._app = this;
            _incomingRequests = new Dictionary<Guid, WorkflowApplication>();
            _outgoingRequests = new Dictionary<Guid, WorkflowApplication>();


        }

        public ListBox GetEventListBox()
        {
            return this.lstEvents;
        }
        private void AddEvent(string szText)
        {
            lstEvents.Items.Add(szText);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // and its network address
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app = (AppSettingsSection)config.GetSection("appSettings");
            string adr = app.Settings["Address"].Value;
            // Display the Branch name on the form
            lblBranch.Content = app.Settings["Branch Name"].Value;
            // Create the ServiceHost
            _sh = new ServiceHost(typeof(ClientService));
            // Add the Endpoint
            string szAddress = "http://localhost:" + adr + "/ClientService";
            System.ServiceModel.Channels.Binding bBinding = new BasicHttpBinding();
            _sh.AddServiceEndpoint(typeof(ILibraryReservation), bBinding, szAddress);
            // Open the ServiceHost to listen for messages
            _sh.Open();

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // Terminate the service host
            _sh.Close();

          
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            // Setup a dictionary object for passing parameters
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Author", txtAuthor.Text);
            parameters.Add("Title", txtTitle.Text);
            parameters.Add("ISBN", txtISBN.Text);
            parameters.Add("Writer", new ListBoxTextWriter(lstEvents));
            WorkflowApplication i = new WorkflowApplication(new SendRequest(), parameters);
            _outgoingRequests.Add(i.Id, i);
            i.Run();

        }

        // Handle the Reserve button click event
        private void Reserve(object sender, RoutedEventArgs e)
        {
            // Get the instanceID from the Tag property
            FrameworkElement fe = (FrameworkElement)sender;
            Guid id = (Guid)fe.Tag;
            ResumeBookmark(id, true);
        }

        // Handle the Cancel button click event
        private void Cancel(object sender, RoutedEventArgs e)
        {
            // Get the instanceID from the Tag property
            FrameworkElement fe = (FrameworkElement)sender;
            Guid id = (Guid)fe.Tag;
            ResumeBookmark(id, false);
        }

        private void ResumeBookmark(Guid id, bool bReserved)
        {
            WorkflowApplication i = _incomingRequests[id];
            try
            {
                i.ResumeBookmark("GetResponse", bReserved);
            }
            catch (Exception e)
            {
                AddEvent(e.Message);
            }
        }

        public void RequestBook(ReservationRequest request)
        {
            // Setup a dictionary object for passing parameters
            Dictionary<string, object> parameters =
                new Dictionary<string, object>();
            parameters.Add("request", request);
            parameters.Add("Writer", new ListBoxTextWriter(lstEvents));

            WorkflowApplication i =
                new WorkflowApplication(new ProcessRequest(), parameters);

            request.InstanceID = i.Id;
            _incomingRequests.Add(i.Id, i);
            i.Run();
        }

        public void RespondToRequest(ReservationResponse response)
        {
            Guid id = response.RequestID;

            WorkflowApplication i = _outgoingRequests[id];
            try
            {
                i.ResumeBookmark("GetResponse", response);
            }
            catch (Exception e2)
            {
                AddEvent(e2.Message);
            }
        }
        public void AddNewRequest(ReservationRequest request)
        {
            this.requestList.Dispatcher.BeginInvoke
                (new Action(() => this.requestList.Items.Add(request)));
        }
    }
}
