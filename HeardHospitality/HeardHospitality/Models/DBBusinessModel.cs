using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace HeardHospitality.Models
{
    public class DBBusinessModel
    {
        public int BusinessRegID { get; }

        public string BusinessName { get; set; }

        public string PhoneNum { get; set; }

        public bool IsVerified { get; set; }

        public string Email { get; set; }
    }

}
