using System;
using System.IO;
using System.Windows.Controls;
using System.Activities;
using BeginWF40.Extension.LeadResponse;

namespace LeadResponse
{
    public static class ApplicationInterface
    {
        public static FollowUpLead _app { get; set; }

        public static void AddAssignment(LeadGenerator.Assignment a)
        {
            if (_app != null)
                _app.lstLeads.Dispatcher.BeginInvoke
                    (new Action(() => _app.AddAssignment(a)));
        }

        public static void UpdateAssignment(LeadGenerator.Assignment a)
        {
            if (_app != null)
                _app.lstLeads.Dispatcher.BeginInvoke
                    (new Action(() => _app.UpdateAssignment(a)));
        }

        public static TextWriter GetStatusWriter()
        {
            if (_app != null)
                return new ListBoxTextWriter(_app.GetEventListBox());
            else
                return null;
        }

        public static void AddEvent(String status)
        {
            if (_app != null)
            {
                GetStatusWriter().WriteLine(status);
            }
        }
    }
}
