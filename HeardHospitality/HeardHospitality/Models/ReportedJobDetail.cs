using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class ReportedJobDetail
    {
        public int ReportedJobDetailID { get; set; }

        public bool AdDetailsIncorrect { get; set; }
        
        public bool PerksListedIncorrect { get; set; }

        public bool SalaryIncorrect { get; set; }

        public bool IllegalExpectations { get; set; }

        public string ? Comments { get; set; }

        public int? JobInfoID { get; set; }
        public JobInfo? JobInfos { get; set; }

        public int? EmployeeID { get; set; }
        public Employee? Employees { get; set; }
    }
}
