using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class Perk
    {
        public int PerkID { get; set; }

        public string PerkName { get; set; }

        public ICollection<JobPerk> JobPerks { get; set; }

    }
}
