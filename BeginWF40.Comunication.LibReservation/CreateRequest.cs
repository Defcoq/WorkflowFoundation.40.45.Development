using System;
using System.Activities;
using System.Configuration;

namespace LibraryReservation
{
    /*****************************************************/
    // This custom activity creates a ReservationRequest
    // class using the input parameters (Title, Author and
    // ISBN).  This is provided in the Request output
    // parameter.  It also returns the network address of
    // the branch that the request should be sent to.
    /*****************************************************/
    public sealed class CreateRequest : CodeActivity
    {
        public InArgument<string> Title { get; set; }
        public InArgument<string> Author { get; set; }
        public InArgument<string> ISBN { get; set; }
        public OutArgument<ReservationRequest> Request { get; set; }
        public OutArgument<string> RequestAddress { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Open the config file and get the Request Address
            Configuration config = ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app =
                (AppSettingsSection)config.GetSection("appSettings");

            // Create a ReservationRequest class and populate 
            // it with the input arguments
            ReservationRequest r = new ReservationRequest
                (
                    Title.Get(context),
                    Author.Get(context),
                    ISBN.Get(context),
                    new Branch
                    {
                        BranchName = app.Settings["Branch Name"].Value,
                        BranchID = new Guid(app.Settings["ID"].Value),
                        Address = app.Settings["Address"].Value
                    }
                );

            // Store the request in the OutArgument
            Request.Set(context, r);

            // Store the address in the OutArgument
            RequestAddress.Set(context, app.Settings["Request Address"].Value);
        }
    }
}
