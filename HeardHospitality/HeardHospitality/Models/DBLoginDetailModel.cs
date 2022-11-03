using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace HeardHospitality.Models
{
    public class DBLoginDetailModel
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string UserPassword { get; set; }

    }
}