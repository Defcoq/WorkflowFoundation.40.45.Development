using System;
using System.Activities;
using LeadGenerator;

namespace LeadResponse
{
    /*****************************************************/
    // This custom activity creates an Assignment class 
    // using the input parameters (LeadID and AsignedTo).  
    /*****************************************************/
    public sealed class CreateAssignment : CodeActivity
    {
        public InArgument<int> LeadID { get; set; }
        public InArgument<string> AssignedTo { get; set; }
        public OutArgument<Assignment> Assignment { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Create an Assignment class and populate its properties
            Assignment a = new Assignment();

            a.WorkflowID = context.WorkflowInstanceId;
            a.LeadID = LeadID.Get(context);
            a.DateAssigned = DateTime.Now;
            a.AssignedTo = AssignedTo.Get(context);
            a.Status = "Assigned";
            a.DateDue = DateTime.Now + TimeSpan.FromDays(5);

            PersistAssignment persist = context.GetExtension<PersistAssignment>();
            persist.AddAssignment(context.WorkflowInstanceId, a, "Insert");

            // Store the request in the OutArgument
            Assignment.Set(context, a);
        }
    }
}
