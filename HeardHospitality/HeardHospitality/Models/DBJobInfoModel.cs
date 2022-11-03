using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class DBJobInfoModel
    {
        public int JobID { get; }

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

        public int BusinessRegID { get; set; }

    }
}
