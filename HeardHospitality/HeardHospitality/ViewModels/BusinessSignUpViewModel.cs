using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class BusinessSignUpViewModel
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string UserPassword { get; set; }

        public string BusinessName { get; set; }

        public string PhoneNum { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string EirCode { get; set; }

        public string Country { get; set; }

    }
}
