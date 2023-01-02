using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HeardHospitality.Models
{
    public class JobInfo
    {
        public int JobInfoID { get; set; }

        public string Title { get; set; }

        public string PositionType { get; set; }


        public double Salary { get; set; }

        public string JobDescription { get; set; }

        public DateTime PostedDate { get; set; }

        public string MinExperience { get; set; }

        public string Category { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Company { get; set; }

        public bool IsActive { get; set; }

        public bool IsReported { get; set; }

        public int BusinessID { get; set; }
        public Business Business { get; set; } 

        public ICollection<JobPerk> JobPerks { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
        public ICollection<ReportedJobDetail> ReportedJobDetails { get; set; }
        
    }
}
