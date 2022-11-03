using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class DBEmployeeModel
    {
        public int EmpRegID { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string EmpBio { get; set; }

        public string DesiredJob { get; set; }

        public bool IsSearching { get; set; }

        public bool IsVisible { get; set; }

        public string Email { get; set; }
    }
}
