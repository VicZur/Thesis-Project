using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HeardHospitality.Models
{
    public class RatingViewModel
    {
        public int RatingID { get; set; }

        public string Company { get; set; }

        public int OverallRating { get; set; }

        public bool WouldWorkAgain { get; set; }

        public int SalaryRating { get; set; }

        public int ManagementRating { get; set; }

        public int FairnessRating { get; set; }

        public int ClienteleRating { get; set; }

        public bool UnpaidTrialShift { get; set; }

        public string Comments { get; set; }

        public DateTime DatePosted { get; set; }

        public bool IsDisplayed { get; set; }

        public bool IsReported { get; set; }

        public int EmployeeExperienceID { get; set; }

        public int BusinessID { get; set; }

        //public ICollection<Reply> Replys { get; set; }

    }
}
