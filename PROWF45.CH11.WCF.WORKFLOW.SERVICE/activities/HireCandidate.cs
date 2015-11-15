using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise1.Core;

namespace Exercise1.CustomActvities
{
   public class HireCandidate:CodeActivity
    {
       [RequiredArgument]
       public InArgument<JobApplicationStatus> ApplicationStatus { get; set; }

       protected override void Execute(CodeActivityContext context)
       {
           var status = context.GetValue(ApplicationStatus);
            //Custom logic for creating a new employee
       }
    }
}
