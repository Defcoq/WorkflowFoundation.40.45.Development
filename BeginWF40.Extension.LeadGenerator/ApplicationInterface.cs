using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using BeginWF40.Extension.LeadGenerator;

namespace LeadGenerator
{
    public static class ApplicationInterface
    {
        public static AddLead _app { get; set; }
        public static void AddEvent(String status)
        {
            if (_app != null)
            {
                new ListBoxTextWriter(_app.GetEventListBox()).WriteLine(status);
            }
        }

        public static void NewLead(Lead l)
        {
            if (_app != null)
                _app.AddNewLead(l);
        }

        public static void UpdateLead(Lead l)
        {
            if (_app != null)
                _app.lstLeads.Dispatcher.BeginInvoke(new Action(() => _app.UpdateLead(l)));
        }

    }
}
