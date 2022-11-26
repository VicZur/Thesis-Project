using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class Address
    {
        public int AddressID { get; set; }

        public string AddressLine1 { get; set; }
        
        public string ? AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string ? EirCode { get; set; }

        public string Country { get; set; }


        public int BusinessID { get; set; }
        public Business Businesses { get; set; }

    }
}
