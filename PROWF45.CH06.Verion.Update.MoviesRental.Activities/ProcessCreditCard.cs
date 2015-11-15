using PROWF45.CH06.Verion.Update.MoviesRental.Activities.Model;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWF45.CH06.Verion.Update.MoviesRental.Activities
{
    public class RunCreditCard : CodeActivity<CreditCard>
    {
        [RequiredArgument]
        public InArgument<CreditCard> inCreditCard { get; set; }
        protected override CreditCard Execute(CodeActivityContext context)
        {
            var ccWithTransNo = inCreditCard.Get(context);
            ccWithTransNo.TransactionNumber = 1542514612;
            return ccWithTransNo;
        }
    }
}
