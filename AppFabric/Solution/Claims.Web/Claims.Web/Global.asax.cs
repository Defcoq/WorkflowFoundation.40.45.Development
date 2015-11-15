using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using System.Threading;
//using Contoso.CustomerServiceDiscoveryProxy;
using System.ServiceModel.Description;

namespace Claims.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static string CarRentalEndpoint;
        public static string DiscoveryProxyStatus;
        public static Thread rentalcarThread;
        public static Thread discoveryProxyThread;
        public static bool Cancel = false;
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "InsuranceClaims",                                              // Route name
                "Claims/Page/{page}",                           // URL with parameters
                new { controller = "Claims", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }


        protected void Session_Start()
        {
            Cancel = false;
            rentalcarThread = null;

            rentalcarThread = new Thread(HostServiceAnnouncements);
            rentalcarThread.Start();

            //discoveryProxyThread = null;
            //discoveryProxyThread = new Thread(HostDiscoveryProxy);
            //discoveryProxyThread.Start();
        }

        protected void Session_End()
        {
            Cancel = true;
        }


        protected void Application_End()
        {
            Cancel = true;
        }

        public static void HostServiceAnnouncements()
        {
            // Create an AnnouncementService instance
            AnnouncementService announcementService = new AnnouncementService();

            // Subscribe the announcement events
            announcementService.OnlineAnnouncementReceived += OnlineAnnouncementReceived;
            announcementService.OfflineAnnouncementReceived += OfflineAnnouncementReceived;

            // Create ServiceHost for the AnnouncementService
            using (ServiceHost announcementServiceHost = new ServiceHost(announcementService))
            {
                // Listen for the announcements sent over UDP multicast
                announcementServiceHost.AddServiceEndpoint(new UdpAnnouncementEndpoint());
                announcementServiceHost.Open();
                Console.WriteLine("Listening for the announcements sent over UDP multicast network...");
                while (Cancel == false)
                    Thread.Sleep(1000000);
            }
        }



        static void OfflineAnnouncementReceived(object sender, AnnouncementEventArgs e)
        {
            CarRentalEndpoint = "";
        }


        static void OnlineAnnouncementReceived(object sender, AnnouncementEventArgs e)
        {
            CarRentalEndpoint = e.EndpointDiscoveryMetadata.Address.Uri.AbsoluteUri;
        }


        //public static void HostDiscoveryProxy()
        //{
        //    Uri probeEndpointAddress = new Uri("net.tcp://localhost:9001/Probe");
        //    Uri announcementEndpointAddress = new Uri("net.tcp://localhost:9091/Announcement");

        //    using (ServiceHost proxyServiceHost = new ServiceHost(new CRMDiscoveryProxy()))
        //    {
        //        DiscoveryEndpoint discoveryEndpoint = new DiscoveryEndpoint(new NetTcpBinding(),
        //                                                    new EndpointAddress(probeEndpointAddress));
        //        discoveryEndpoint.IsSystemEndpoint = false;
        //        AnnouncementEndpoint announcementEndpoint = new AnnouncementEndpoint(new NetTcpBinding(),
        //                                                        new EndpointAddress(announcementEndpointAddress));
        //        proxyServiceHost.AddServiceEndpoint(discoveryEndpoint);
        //        proxyServiceHost.AddServiceEndpoint(announcementEndpoint);
        //        proxyServiceHost.Open();

        //        System.Text.StringBuilder sb = new System.Text.StringBuilder("Discovery Proxy is listening on: ");
        //        sb.Append(String.Format("{0} (Probe) and on: ", proxyServiceHost.Description.Endpoints[0].Address));
        //        sb.Append(String.Format("{0} (Announcement)", proxyServiceHost.Description.Endpoints[1].Address));

        //        DiscoveryProxyStatus = sb.ToString();

        //        while (Cancel == false)
        //            Thread.Sleep(1000000);
        //    }
        //}



    }
}