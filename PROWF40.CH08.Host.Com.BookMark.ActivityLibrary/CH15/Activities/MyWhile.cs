using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH15.Activities_designer;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH15.Activities
{

    [Designer(typeof(MyWhileDesigner))]
    public sealed class MyWhile : NativeActivity
    {
        [Browsable(false)]
        public Activity Body { get; set; }
        public Activity<Boolean> Condition { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            throw new NotImplementedException();
        }

    }
}
