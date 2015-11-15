using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ServiceModel;

namespace WorkflowActivities
{

    public sealed class BillingActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<int> ClaimId { get; set; }
        public InArgument<String> WCFEndpoint { get; set; }
        public OutArgument<string> Status { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            int claimId = context.GetValue(this.ClaimId);
            EndpointAddress address = new EndpointAddress(context.GetValue(this.WCFEndpoint));

            //Set up the request parameters
            BillingService.ProcessClaimRequest request = new BillingService.ProcessClaimRequest();
            request.claimId = claimId;

            //Create WCF channel and invoke the WCF operation
            ChannelFactory<BillingService.BillingService> channelFactory = 
                new ChannelFactory<BillingService.BillingService>(new BasicHttpBinding(), address);
            BillingService.BillingService channel = channelFactory.CreateChannel();
            BillingService.ProcessClaimResponse response = channel.ProcessClaim(request);

            channelFactory.Close();

            //Set up the return value
            Status.Set(context, response.ProcessClaimResult);

        }
    }
}
