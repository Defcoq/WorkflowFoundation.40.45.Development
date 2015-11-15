using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROWF45.CH11.WCF.WORKFLOW.CLIENT.SERVICE
{
    public partial class Form1 : Form
    {
        JobPostingService.JobApplicationClient jobApplicationService = null;
        private string JobApplicationRef = string.Empty;

        public Form1()
        {
            InitializeComponent();
            jobApplicationService = new JobPostingService.JobApplicationClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var application = new JobPostingService.JobApplication()
             {

                 ApplyingCandidate = new JobPostingService.Candidate
                 {
                     FirstName = "Jim Deffo",
                     LastName = "Smith Jean Pierre",
                     EmailAddress = "jim.smith@acme.com",
                     SSN = "555-45-4444",
                    
                 },
                 JobPostingId = 5,
                
             };
            JobApplicationRef = jobApplicationService.ApplyForJob(application);

            jobApplicationService.InterviewCandidate(JobApplicationRef, checkBox1.Checked);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var application = jobApplicationService.GetJobApplicationStatus(JobApplicationRef);

        }
    }
}
