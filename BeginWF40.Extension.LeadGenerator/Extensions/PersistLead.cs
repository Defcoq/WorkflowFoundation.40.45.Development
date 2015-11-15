using System;
using System.Activities.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Transactions;
using System.Xml.Linq;


namespace LeadGenerator.Extensions
{
    public class PersistLead : PersistenceParticipant
    {
        private string _connectionString;
        private IDictionary<Guid, Lead> _object;
        private IDictionary<Guid, string> _action;
        public PersistLead(string connectionString)
        {
            _connectionString = connectionString;
            _object = new Dictionary<Guid, Lead>();
            _action = new Dictionary<Guid, string>();
        }
        internal void AddLead(Lead l, string action)
        {
            _object.Remove(l.WorkflowID);
            _action.Remove(l.WorkflowID);
            _object.Add(l.WorkflowID, l);
            _action.Add(l.WorkflowID, action);
        }

        protected override void CollectValues(out IDictionary<XName, object> readWriteValues, out IDictionary<XName, object> writeOnlyValues)
        {
            // We're not actually providing data to the caller
            readWriteValues = null;
            writeOnlyValues = null;
            // See if there is any work to do...
            if (_object.Count > 0)
            {
                // Get the current transaction
                Transaction t = System.Transactions.Transaction.Current;
                // Setup the DataContext Open the connection, if necessary
                LeadDataDataContext dc = new LeadDataDataContext(_connectionString);

                if (dc.Connection.State == System.Data.ConnectionState.Closed)
                    dc.Connection.Open();
                if (t != null)
                    dc.Connection.EnlistTransaction(t);
                // Process each object in our work queue
                foreach (KeyValuePair<Guid, Lead> kvp in _object)
                {
                    Lead l = kvp.Value as Lead;
                    string action = _action[l.WorkflowID];
                    // Perform the insert
                    if (action == "Insert")
                    {
                        dc.Leads.InsertOnSubmit(l);
                    }
                    // Perform the update
                    if (action == "Update")
                    {
                        dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Leads);
                        Lead lTmp = dc.Leads.SingleOrDefault<Lead>
                        (x => x.WorkflowID == l.WorkflowID);
                        if (lTmp != null)
                        {
                            lTmp.AssignedTo = l.AssignedTo;
                            lTmp.Status = l.Status;
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
