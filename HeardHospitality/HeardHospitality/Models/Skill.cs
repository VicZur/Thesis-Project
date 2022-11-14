using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class Skill
    {
        public int SkillID { get; set; }

        public string SkillDescription { get; set; }

        public ICollection<EmpSkill> EmpSkills { get; set; }

    }
}
