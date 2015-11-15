using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Description;
using System.ServiceModel.Description;
using System.Activities;
using System.Xml.Linq;
using System.Configuration;
using LibraryReservation;


namespace BeginWF40.Comunication.LibReservation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Open the config file and get the name for this branch
            // and its network address
            Configuration config = ConfigurationManager
            .OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app =
            (AppSettingsSection)config.GetSection("appSettings");
            string adr = app.Settings["Address"].Value;
            Console.WriteLine(app.Settings["Branch Name"].Value);
            // Create a service to handle incoming requests
            WorkflowService service = new WorkflowService
            {
                Name = "LibraryReservation",
                Body = new ProcessRequest(),
                Endpoints =
                        {
                                new Endpoint
                                {
                                ServiceContractName="ILibraryReservation",
                                AddressUri = new Uri("http://localhost:" + adr +"/LibraryReservation"),
                                Binding = new BasicHttpBinding(),
                                }
                        }
            };

            // Create a WorkflowServiceHost that listens for incoming messages
            System.ServiceModel.Activities.WorkflowServiceHost wsh = new System.ServiceModel.Activities.WorkflowServiceHost(service);
            wsh.Open();


            Console.WriteLine("Waiting for requests, press ENTER to send a request.");
            Console.ReadLine();
            // Create dictionary with input arguments for the workflow
            IDictionary<string, object> input = new Dictionary<string, object>
                                            {
                                                { "Title" , "Gone with the Wind" },
                                                { "Author", "Margaret Mitchell" },
                                                { "ISBN", "9781416548898" }
                                            };
            // Invoke the SendRequest workflow
            IDictionary<string, object> output = WorkflowInvoker.Invoke(new SendRequest(), input);
            ReservationResponse resp = (ReservationResponse)output["Response"];
            // Display the response
            Console.WriteLine("Response received from the {0} branch",resp.Provider.BranchName);
            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            // Close the WorkflowSe



        }
    }
}
