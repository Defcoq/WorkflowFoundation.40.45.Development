using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace WorkflowActivities
{

    public sealed class FindRentalCarActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<int> ClaimId { get; set; }
        public InArgument<String> WCFEndpoint { get; set; }
        public OutArgument<String> Confirmation { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            int claimId = context.GetValue(this.ClaimId);
            EndpointAddress address = new EndpointAddress(context.GetValue(this.WCFEndpoint));

            ////Create DiscoveryClient
            //DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            ////Create the search criteria for the specified scope
            //FindCriteria findCriteria = new FindCriteria(typeof(CarRentalService.CarRentalService));

            ////Find BillingService endpoint
            //FindResponse findResponse = discoveryClient.Find(findCriteria);

            //if (findResponse.Endpoints.Count == 0) throw new EndpointNotFoundException();

            ////Pick the first discovered endpoint
            //EndpointAddress address = findResponse.Endpoints[0].Address;


            //Set up the request parameters
            CarRentalService.FindRentalCarRequest request = new CarRentalService.FindRentalCarRequest();
            request.claimId = claimId;

            //Create WCF channel and invoke the WCF operation
            //ChannelFactory<CarRentalService.CarRentalService> channelFactory = 
            //    new ChannelFactory<CarRentalService.CarRentalService>("BasicHttpBinding_CarRentalService");
            ChannelFactory<CarRentalService.CarRentalService> channelFactory =
                new ChannelFactory<CarRentalService.CarRentalService>(new BasicHttpBinding(), address);
            CarRentalService.CarRentalService channel = channelFactory.CreateChannel();
            CarRentalService.FindRentalCarResponse response = channel.FindRentalCar(request);

            channelFactory.Close();

            //Set up the return value
            Confirmation.Set(context, response.FindRentalCarResult);

        }
    }
}
