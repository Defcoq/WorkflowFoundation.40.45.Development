using System;
using System.Activities.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Transactions;
using System.Xml.Linq;


namespace LeadGenerator.Extensions
{
    public class PersistAssignment : PersistenceParticipant
    {
        private string _connectionString;

        private IDictionary<Guid, Assignment> _object;
        private IDictionary<Guid, string> _action;
        public PersistAssignment(string connectionString)
        {
            _connectionString = connectionString;
            _object = new Dictionary<Guid, Assignment>();
            _action = new Dictionary<Guid, string>();
        }
        internal void AddAssignment(Guid id, Assignment a, string action)
        {
            // Make sure there isn't one already here
            _object.Remove(id);
            _action.Remove(id);
            _object.Add(id, a);
            _action.Add(id, action);
        }
        protected override void CollectValues
        (out IDictionary<XName, object> readWriteValues,
        out IDictionary<XName, object> writeOnlyValues)
        {
            // We're not actually providing data to the caller
            readWriteValues = null;
            writeOnlyValues = null;
            // See if there is any work to do...
            if (_object.Count > 0)
            {
                // Get the current transaction
                Transaction t = System.Transactions.Transaction.Current;
                // Setup the DataContext
                LeadDataDataContext dc = new LeadDataDataContext(_connectionString);
                // Open the connection, if necessary
                if (dc.Connection.State == System.Data.ConnectionState.Closed)
                    dc.Connection.Open();
                if (t != null)
                    dc.Connection.EnlistTransaction(t);
                // Process each object in our work queue
                foreach (KeyValuePair<Guid, Assignment> kvp in _object)
                {
                    Assignment a = kvp.Value as Assignment;
                    string action = _action[kvp.Key];
                    // Perform the insert
                    if (action == "Insert")
                    {
                        dc.Assignments.InsertOnSubmit(a);
                    }
                    // Perform the update
                    if (action == "Update")
                    {
                        dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Leads);
                        Assignment aTmp = dc.Assignments
                        .SingleOrDefault<Assignment>
                        (x => x.WorkflowID == kvp.Key);
                        if (aTmp != null)
                        {
                            aTmp.DateCompleted = a.DateCompleted;
                            aTmp.Remarks = a.Remarks;
                            aTmp.Status = a.Status;
                        }
                    }
                }
                // Submit all the changes to the database
                dc.SubmitChanges();
                // Remove all objects since the changes have been submitted
                _object.Clear();
                _action.Clear();
            }
        }

    }
}
