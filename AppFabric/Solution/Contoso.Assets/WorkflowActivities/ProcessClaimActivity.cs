using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ServiceModel;
using BusinessEntities;

namespace WorkflowActivities
{

    public sealed class ProcessClaimActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<Claim> Claim { get; set; }
        public InArgument<String> WCFEndpoint { get; set; }
        public OutArgument<string> Result { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            Claim claim = context.GetValue(this.Claim);
            EndpointAddress address = new EndpointAddress(context.GetValue(this.WCFEndpoint));

            //Set up the request parameters
            AppraisalService.SaveRequest request = new AppraisalService.SaveRequest();
            request.claim = claim;

            //Create WCF channel and invoke the WCF operation
            ChannelFactory<AppraisalService.AppraisalService> channelFactory = 
                new ChannelFactory<AppraisalService.AppraisalService>(new BasicHttpBinding(), address);
            AppraisalService.AppraisalService channel = channelFactory.CreateChannel();
            AppraisalService.SaveResponse response = channel.Save(request);

            channelFactory.Close();

            //Set up the return value
            Result.Set(context, response.SaveResult);

        }
    }
}
