using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class DBRatingModel
    {
        public int RatingID { get; }

        public int OverallRating { get; set; }

        public bool WouldWorkAgain { get; set; }

        public int SalaryRating { get; set; }

        public int ManagementRating { get; set; }

        public int FairnessRating { get; set; }

        public int ClienteleRating { get; set; }

        public bool UnpaidTrialShift { get; set; }

        public string Comments { get; set; }

        //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        // come back and check this if DateTime not working
        public DateTime DatePosted { get; set; }

        public bool IsDisplayed { get; set; }

        public int ExperienceID { get; set; }

        public int BusinessRegID { get; set; }

    }
}
