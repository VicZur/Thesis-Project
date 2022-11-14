using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class EmployeeExperience
    {
        public int EmployeeExperienceID { get; set; }

        public string JobTitle { get; set; }

        public string PositionType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Company { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public bool IsVerified { get; set; }

        public bool DisplayOnProfile { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        ICollection<Rating> Ratings { get; set; }

    }
}
