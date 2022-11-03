using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class DBJobApplicationModel
    {
        public int ApplicationID { get; }

        public DateTime DateApplied { get; set; }

        public bool IsReviewed { get; set; }

        public int EmpRegID { get; set; }

        public int JobID { get; set; }
    }
}
