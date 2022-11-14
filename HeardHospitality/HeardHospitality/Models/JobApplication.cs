using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class JobApplication
    {
        public int JobApplicationID { get; set; }

        public DateTime DateApplied { get; set; }

        public bool IsReviewed { get; set; }


        
        public int? EmployeeID { get; set; }
        public Employee? Employee { get; set; }




        public int? JobInfoID { get; set; }
        public JobInfo? JobInfo { get; set; }
    }
}