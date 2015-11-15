using System;
using System.Activities;
using System.Configuration;

namespace LibraryReservation
{
    /*****************************************************/
    // This custom activity creates a ReservationResponse
    // class.  The original request is provided as an 
    // InArgument as well as a boolean to indicate if the
    // request was satisfied or not.  The class is provided 
    // in the Response OutArgument.
    /*****************************************************/
    public sealed class CreateResponse : CodeActivity
    {
        public InArgument<ReservationRequest> Request { get; set; }
        public InArgument<bool> Reserved { get; set; }
        public OutArgument<ReservationResponse> Response { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Open the config file
            Configuration config = ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app =
                (AppSettingsSection)config.GetSection("appSettings");

            // Create the ReservationResponse class and populate it
            ReservationResponse r = new ReservationResponse
                (
                    Request.Get(context),
                    Reserved.Get(context),
                    new Branch
                    {
                        BranchName = app.Settings["Branch Name"].Value,
                        BranchID = new Guid(app.Settings["ID"].Value),
                        Address = app.Settings["Address"].Value
                    }
                );

            // Store the Response in the OutArgument
            Response.Set(context, r);
        }
    }
}
