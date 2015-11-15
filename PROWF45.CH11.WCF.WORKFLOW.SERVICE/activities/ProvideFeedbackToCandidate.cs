using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.CustomActvities
{
   public class ProvideFeedbackToCandidate:CodeActivity
    {
       [RequiredArgument]
       public InArgument<string> MessageToCandidate { get; set; }
       [RequiredArgument]
       public InArgument<string> EmailAddress { get; set; }

       protected override void Execute(CodeActivityContext context)
       {
           var message = context.GetValue(MessageToCandidate);
           var address = context.GetValue(EmailAddress);
           //Custom logic for creating a new employee
       }
    }
}
