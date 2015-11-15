using System;
using System.Activities;
using LeadGenerator;

namespace LeadResponse
{
    /*****************************************************/
    // This custom activity completes an Assignment.  
    /*****************************************************/
    public sealed class CompleteAssignment : CodeActivity
    {
        public InOutArgument<Assignment> Assignment { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Assignment a = Assignment.Get(context);
            a.Status = "Completed";
            a.DateCompleted = DateTime.Now;

            PersistAssignment persist = context.GetExtension<PersistAssignment>();
            persist.AddAssignment(context.WorkflowInstanceId, a, "Update");

            // Store the request in the OutArgument
            Assignment.Set(context, a);
        }
    }
}
