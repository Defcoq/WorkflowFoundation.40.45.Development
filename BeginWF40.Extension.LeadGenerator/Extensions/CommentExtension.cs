using System;
using System.Activities.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeadGenerator.Extensions
{
    public class CommentExtension : PersistenceParticipant
    {
        private string _comments = "";
        public string Comments { get { return _comments; } }
        internal void AddComment(string s)
        {
            if (_comments.Length > 1)
                _comments += "\r\n";
            this._comments += s;
        }
        protected override void CollectValues(out IDictionary<XName, object> readWriteValues, out IDictionary<XName, object> writeOnlyValues)
        {
            readWriteValues = new Dictionary<XName, object>(1)
        {
           { "Comment", this._comments }
        };
            writeOnlyValues = null;
        }
        protected override void PublishValues
        (IDictionary<XName, object> readWriteValues)
        {
            object loadedData;
            if (readWriteValues.TryGetValue("Comment", out loadedData))
            {
                this._comments = (string)loadedData;
            }
        }

    }
}
