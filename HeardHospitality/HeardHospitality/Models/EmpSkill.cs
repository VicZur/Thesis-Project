using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class EmpSkill
    {
        public int EmpSkillID { get; set; }

        public DateTime DateAdded { get; set; }

        public int SkillID { get; set; }
        public Skill Skill { get; set; }    

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }


    }
}
