using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary
{

    public sealed class GetString : NativeActivity<String>
    {
        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark("GetString", Resumed);
        }

        private void Resumed(NativeActivityContext context,Bookmark bookmark, object value)
        {
            if (value != null && value is String)
            {
                Result.Set(context, value as String);
            }
        }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }
    }
}
