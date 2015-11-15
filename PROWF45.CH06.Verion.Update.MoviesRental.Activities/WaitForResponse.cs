using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace PROWF45.CH06.Verion.Update.MoviesRental.Activities
{

    public sealed class WaitForResponse<TResult> : NativeActivity<TResult>
    {
        public WaitForResponse()
            : base()
        {

        }

        public string ResponseName { get; set; }

        protected override bool CanInduceIdle
        { //override when the custom activity is allowed to make he workflow go idle
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark(this.ResponseName, new BookmarkCallback(this.ReceivedResponse));
        }

        void ReceivedResponse(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            this.Result.Set(context, (TResult)obj);
        }
    }
}
