using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq;


namespace LeadGenerator.Extensions
{
   public  class SqlTrackingParticipant : TrackingParticipant
    {
        private string _connectionString { get; set; }
        private const String participantName = "SqlTrackingParticipant";

        public SqlTrackingParticipant(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            WorkflowInstanceRecord instanceTrackingRecord =
                record as WorkflowInstanceRecord;
            if (instanceTrackingRecord != null)
            {
                TrackInstance t = new TrackInstance();

                t.WorkflowID = instanceTrackingRecord.InstanceId;
                t.Status = instanceTrackingRecord.State;
                t.EventDate = DateTime.UtcNow;

                // Insert a record into the TrackInstance table
                LeadDataDataContext dc =
                    new LeadDataDataContext(_connectionString);
                dc.TrackInstances.InsertOnSubmit(t);
                dc.SubmitChanges();
            }

            BookmarkResumptionRecord bookTrackingRecord =
                record as BookmarkResumptionRecord;

            if (bookTrackingRecord != null)
            {
                TrackBookmark t = new TrackBookmark();

                t.WorkflowID = bookTrackingRecord.InstanceId;
                t.BookmarkName = bookTrackingRecord.BookmarkName;
                t.EventDate = DateTime.UtcNow;

                // Insert a record into the TrackBookmark table
                LeadDataDataContext dc =
                    new LeadDataDataContext(_connectionString);
                dc.TrackBookmarks.InsertOnSubmit(t);
                dc.SubmitChanges();
            }

            ActivityStateRecord activityStateRecord =
                record as ActivityStateRecord;
            if (activityStateRecord != null)
            {
                TrackActivity t = new TrackActivity();

                t.ActivityName = activityStateRecord.Activity.Name;
                t.WorkflowID = activityStateRecord.InstanceId;
                t.Status = activityStateRecord.State;
                t.EventDate = DateTime.UtcNow;

                // Concatenate all the variables into a string
                IDictionary<String, object> variables =
                    activityStateRecord.Variables;
                StringBuilder s = new StringBuilder();

                if (variables.Count > 0)
                {
                    foreach (KeyValuePair<string, object> v in variables)
                    {
                        s.AppendLine(String.Format("{0}: Value = [{1}]",
                            v.Key, v.Value));
                    }
                }

                // Store the variables string
                t.Variables = s.ToString();

                // Insert a record into the TrackActivity table
                LeadDataDataContext dc =
                    new LeadDataDataContext(_connectionString);
                dc.TrackActivities.InsertOnSubmit(t);
                dc.SubmitChanges();
            }

            CustomTrackingRecord customTrackingRecord =
                record as CustomTrackingRecord;

            if (customTrackingRecord != null)
            {
                TrackCustom t = new TrackCustom();

                t.WorkflowID = customTrackingRecord.InstanceId;
                t.CustomEventName = customTrackingRecord.Name;
                t.EventDate = DateTime.UtcNow;

                // Concatenate all the user data into a string
                string s = "";
                if ((customTrackingRecord != null) &&
                    (customTrackingRecord.Data.Count > 0))
                {
                    foreach (string data in customTrackingRecord.Data.Keys)
                    {
                        if (s.Length > 1)
                            s += "\r\n";
                        s += String.Format("{0}: Value = [{1}]", data,
                            customTrackingRecord.Data[data]);
                    }
                }
                t.UserData = s;

                // Insert a record into the TrackUser table
                LeadDataDataContext dc =
                    new LeadDataDataContext(_connectionString);
                dc.TrackCustoms.InsertOnSubmit(t);
                dc.SubmitChanges();
            }
        }
    }
}
