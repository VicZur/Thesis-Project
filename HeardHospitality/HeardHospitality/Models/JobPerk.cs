using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class JobPerk
    {
        public int JobPerkID {get; set;}

        public string Details { get; set; }

        public int PerkID { get; set; }
        public Perk Perk { get; set; }

        public int JobInfoID { get; set ;}
        public JobInfo JobInfo { get; set; }
    }
}
