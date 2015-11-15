using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PROWF45.CH06.VERSIONING.UPDATE.WORKFLOW.Model
{
    [DataContract]
    public class TeachingApplication
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int YearsOfExperience { get; set; }

    }
}