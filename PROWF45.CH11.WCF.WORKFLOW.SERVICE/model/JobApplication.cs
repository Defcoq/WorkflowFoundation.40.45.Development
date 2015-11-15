using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise1.Core
{
    public class JobApplicationStatus
    {
        public Guid JobApplicationId { get; set; }
        public JobApplication SubmittedApplication { get; set; }
        public bool PassedInterview { get; set; }
        public bool PassedBackgroundCheck { get; set; }
        public JobPosting JobAppliedTo { get; set; }
    }
    public class JobApplication
    {
        public Candidate ApplyingCandidate { get; set; }
        public int JobPostingId { get; set; }
    }

    public class Candidate
    {
        public int CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string SSN { get; set; }
    }

    public class JobPosting
    {
        public int JobPostingId { get; set; }
        public string PositionName { get; set; }
        public DateTime PostingDate { get; set; }
        public ContactManager HiringManager { get; set; }
    }

    public class ContactManager
    {
        public int ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}