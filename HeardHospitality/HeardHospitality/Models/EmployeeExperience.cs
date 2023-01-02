using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HeardHospitality.Models
{
    public class EmployeeExperience
    {
        public int EmployeeExperienceID { get; set; }

        public string JobTitle { get; set; }

        public string Details { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Company { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public bool IsVerified { get; set; }

        public bool DisplayOnProfile { get; set; }

        public int ? EmployeeID { get; set; }
        public Employee ? Employee { get; set; }

        //[ForeignKey("Business")]
        //[ForeignKey("Business")]
        //[OnDelete(DeleteBehavior.NoAction)]
        public int ? BusinessID { get; set; }

        public Business ? Business { get; set; }



        public ICollection<Rating> Ratings { get; set; }

    }
}
