using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HeardHospitality.Models
{
    public class Employee 
    {

        public int EmployeeID { get; set; }

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

        //public int LoginDetailID { get; set; }
        public LoginDetail LoginDetails { get; set; }


        public ICollection<JobApplication> JobApplications { get; set; }
        public ICollection<EmpSkill> EmpSkills { get; set; }
        public ICollection<EmployeeExperience> EmployeeExperiences { get; set; }
    }
}
