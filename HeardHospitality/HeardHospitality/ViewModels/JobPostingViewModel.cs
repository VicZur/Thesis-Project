using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace HeardHospitality.Models
{
    public class JobPostingViewModel
    {
        public int BusinessID { get; set; }

        public int JobInfoID { get; set; }

        public int JobPerkID { get; set; }

        public int PerkID { get; set; }


        //From Business Model
        public string BusinessName { get; set; }

        public string PhoneNum { get; set; }


        //From Address Model
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string EirCode { get; set; }

        public string Country { get; set; }


        //From JobInfo Model
        public string Title { get; set; }

        public string PositionType { get; set; }


        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double Salary { get; set; }

        public string JobDescription { get; set; }

        public DateTime PostedDate { get; set; }

        public string MinExperience { get; set; }

        public string Category { get; set; }

        public string Company { get; set; }

        public bool IsActive { get; set; }

        public bool IsReported { get; set; }

        public List<PerkViewModel> Perks { get; set; }

    }

    //public class PerkViewModel
    //{
    //    public string PerkName { get; set; }

    //    public string Details { get; set; }
    //}
}
