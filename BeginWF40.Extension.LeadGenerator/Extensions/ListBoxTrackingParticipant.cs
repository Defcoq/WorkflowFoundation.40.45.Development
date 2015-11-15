using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Tracking;
using System.Windows.Controls;


namespace LeadGenerator.Extensions
{
   public  class ListBoxTrackingParticipant : TrackingParticipant
    {
        private ListBox _eventLog;
        private const String participantName = "ListBoxTrackingParticipant";
        public ListBoxTrackingParticipant(ListBox listBox)
        {
            _eventLog = listBox;
        }
        private void AddEvent(String msg)
        {
            if (_eventLog != null)
                _eventLog.Dispatcher.BeginInvoke(new Action(() => _eventLog.Items.Add(msg)));
        }

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            // Log header information
            AddEvent(String.Format("Type: {0} Level: {1}, RecordNumber: {2}",
            record.GetType().Name, record.Level, record.RecordNumber));
            // If this is a instance record
            WorkflowInstanceRecord instance = record as WorkflowInstanceRecord;
            if (instance != null)
            {
                AddEvent(String.Format(" InstanceID: {0} State: {1}",instance.InstanceId, instance.State));
            }
            // If this is a bookmark record
            BookmarkResumptionRecord bookmark = record as BookmarkResumptionRecord;
            if (bookmark != null)
            {
                AddEvent(String.Format(" Bookmark {0} resumed",bookmark.BookmarkName));
            }

            // If this is an activity record
            ActivityStateRecord activity = record as ActivityStateRecord;
            if (activity != null)
            {
                IDictionary<String, object> variables = activity.Variables;
                StringBuilder s = new StringBuilder();

                if (variables.Count > 0)
                {
                    s.AppendLine(" Variables:");
                    foreach (KeyValuePair<string, object> v in variables)
                    {
                        s.AppendLine(String.Format(" {0} Value: [{1}]",v.Key, v.Value));
                    }

                }
                AddEvent(String.Format(" Activity: {0} State: {1} {2}",activity.Activity.Name, activity.State, s.ToString()));

            }

            // If this is a user record
            CustomTrackingRecord user = record as CustomTrackingRecord;
            if ((user != null) && (user.Data.Count > 0))
            {
                AddEvent(String.Format(" User Data: {0}", user.Name));
                foreach (string data in user.Data.Keys)
                {
                    AddEvent(String.Format(" {0} : {1}", data, user.Data[data]));
                }
            }


        }
    }
}
