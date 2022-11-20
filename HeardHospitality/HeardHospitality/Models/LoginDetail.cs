using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class LoginDetail : IdentityUser
    {
//       public int LoginDetailId { get; set; }

//        public string Email { get; set; }

//        public string NormalizedEmail { get; set; }

//        public string Username { get; set; }

//        public string NormalizedUserName { get; set; }

//        public string PasswordHash { get; set; }
          public DateTime DateCreated { get; set; }
        
          public bool isBusinessAccount { get; set; }
          public bool isEmployeeAccount { get; set; }

//        public bool EmailConfirmed { get; set; }

//        public ICollection<Employee> Employee { get; set; }

//        public ICollection<Business> Business { get; set; }

    }

}
