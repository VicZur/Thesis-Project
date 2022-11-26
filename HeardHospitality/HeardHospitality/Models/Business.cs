using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

namespace HeardHospitality.Models
{
    public class Business
    {
        public int BusinessID { get; set; }

        public string BusinessName { get; set; }

        public string PhoneNum { get; set; }

        public bool IsVerified { get; set; }

        //FK details to DBLoginDetailModel
        //public int LoginDetailId { get; set; }

        [Required]
        public LoginDetail LoginDetails { get; set; }


        //Tables that reference DBBusinessModel as their FK
        public ICollection<Address> Addresses { get; set;}

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<Reply> Replys { get; set; }

        public ICollection<JobInfo> JobInfos { get; set; }

    }
}
