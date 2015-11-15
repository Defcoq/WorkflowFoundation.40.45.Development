using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace Contoso.CustomerServiceDiscoveryProxy
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CRMDiscoveryProxy : DiscoveryProxy 
    {
        Dictionary<EndpointAddress, EndpointDiscoveryMetadata> onlineServices = 
            new Dictionary<EndpointAddress,EndpointDiscoveryMetadata>();
        DiscoveryResponse response = new DiscoveryResponse();

        protected override IAsyncResult OnBeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state)
        {
            return response;
        }

        protected override IAsyncResult OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
        {
            if (this.onlineServices.Count > 0)
            {
                lock (this.onlineServices)
                {
                    this.onlineServices.Remove(endpointDiscoveryMetadata.Address);
                    PrintTrace(endpointDiscoveryMetadata, "Removing");
                }
            }

            return response;

        }

        protected override IAsyncResult OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
        {
            lock (this.onlineServices)
            {
                this.onlineServices[endpointDiscoveryMetadata.Address] =
                    endpointDiscoveryMetadata;
                PrintTrace(endpointDiscoveryMetadata, "Adding");
            }

            return response;
        }

        protected override IAsyncResult OnBeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
        {
            return response;
        }

        protected override EndpointDiscoveryMetadata OnEndResolve(IAsyncResult result)
        {
            if (onlineServices.Count > 0)
            {
                return onlineServices.GetEnumerator().Current.Value;
            }
            return null;
        }



        private void PrintTrace(EndpointDiscoveryMetadata endpointDiscoveryMetadata, string message)
        {
            Console.WriteLine("{0} {1}", message, endpointDiscoveryMetadata.Address.Uri.AbsoluteUri);
            System.Diagnostics.Debug.WriteLine("{0} {1}", message, endpointDiscoveryMetadata.Address.Uri.AbsoluteUri);
        }

        protected override void OnEndFind(IAsyncResult result)
        {
        }

        protected override void OnEndOfflineAnnouncement(IAsyncResult result)
        {
        }

        protected override void OnEndOnlineAnnouncement(IAsyncResult result)
        {
        }


    }


    public class DiscoveryResponse : IAsyncResult
    {
        public object AsyncState
        {
            get { throw new NotImplementedException(); }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { throw new NotImplementedException(); }
        }
    }

}
