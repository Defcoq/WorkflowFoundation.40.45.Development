using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadGenerator
{
   public  class WaitForInput<T> : NativeActivity<T>

    {
        public WaitForInput()
            : base()
        {
        }
        public string BookmarkName { get; set; }
        public OutArgument<T> Input { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark(BookmarkName, new BookmarkCallback(this.Continue));
        }
        void Continue(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            Input.Set(context, (T)obj);
        }
        protected override bool CanInduceIdle { get { return true; } }


      
    }
}
