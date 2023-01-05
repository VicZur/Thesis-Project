using HeardHospitality.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HeardHospitality.Controllers
{
    public class RatingController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<LoginDetail> _userManager;

        public RatingController(IConfiguration config, UserManager<LoginDetail> userManager)
        {
            _configuration = config;
            _userManager = userManager;
        }


        public IActionResult All(Rating r, int busID)
        {

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            string query = "SELECT * FROM Rating WHERE Rating.BusinessID = @BusinessID AND IsDisplayed = 1 ORDER BY DatePosted";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BusinessID", busID);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Rating> ratings_List = new List<Rating>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ratings_List.Add(new Rating
                    {
                        RatingID = Convert.ToInt32(rdr["RatingID"]),
                        OverallRating = Convert.ToInt32(rdr["OverallRating"]),
                        WouldWorkAgain = Convert.ToBoolean(rdr["WouldWorkAgain"]),
                        SalaryRating = Convert.ToInt32(rdr["SalaryRating"]),
                        ManagementRating = Convert.ToInt32(rdr["ManagementRating"]),
                        FairnessRating = Convert.ToInt32(rdr["FairnessRating"]),
                        ClienteleRating = Convert.ToInt32(rdr["ClienteleRating"]),
                        UnpaidTrialShift = Convert.ToBoolean(rdr["UnpaidTrialShift"]),
                        Comments = Convert.ToString(rdr["Comments"]),
                        DatePosted = Convert.ToDateTime(rdr["DatePosted"]),
                        IsDisplayed = Convert.ToBoolean(rdr["IsDisplayed"]),
                        EmployeeExperienceID = Convert.ToInt32(rdr["EmployeeExperienceID"]),
                        //BusinessID = Convert.ToInt32(rdr["BusinessID"]),
                    });
                }
            }

            conn.Close();


            return View(ratings_List);
        }

        public IActionResult CheckIfRated(RatingViewModel r, int busID, int empExpID)
        {
            var rating = new RatingViewModel { BusinessID = busID, EmployeeExperienceID = empExpID };

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            string query = "SELECT * FROM Rating where EmployeeExperienceID = @EmployeeExperienceID and BusinessID = @BusinessID";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@EmployeeExperienceID", empExpID);
            cmd.Parameters.AddWithValue("@BusinessID", busID);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)//This user has already left a review
            {
                while (rdr.Read())
                {
                    rating.RatingID = Convert.ToInt32(rdr["RatingID"]);
                    rating.OverallRating = Convert.ToInt32(rdr["OverallRating"]);
                    rating.WouldWorkAgain = Convert.ToBoolean(rdr["WouldWorkAgain"]);
                    rating.SalaryRating = Convert.ToInt32(rdr["SalaryRating"]);
                    rating.ManagementRating = Convert.ToInt32(rdr["ManagementRating"]);
                    rating.FairnessRating = Convert.ToInt32(rdr["FairnessRating"]);
                    rating.ClienteleRating = Convert.ToInt32(rdr["ClienteleRating"]);
                    rating.UnpaidTrialShift = Convert.ToBoolean(rdr["UnpaidTrialShift"]);
                    rating.Comments = Convert.ToString(rdr["Comments"]);
                    rating.DatePosted = Convert.ToDateTime(rdr["DatePosted"]);
                    rating.IsDisplayed = Convert.ToBoolean(rdr["IsDisplayed"]);
                    rating.EmployeeExperienceID = Convert.ToInt32(rdr["EmployeeExperienceID"]);

                }

                string message = "You have already left a review for this business.";
                return View("SeeRating", rating);
            }
            conn.Close();
            if (rating.Comments == null)
            {
                return View("New", rating);
            }
            return RedirectToAction("ViewEmployeeExperience", "EmployeeExperience");
        }



        [Authorize(Roles = "employeeuser")]
        public IActionResult New(RatingViewModel r, int busID, int empExpID)
        {
            var rating = new RatingViewModel { BusinessID = busID, EmployeeExperienceID = empExpID };

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            string query = "SELECT BusinessName FROM dbo.Business WHERE BusinessID = @BusinessID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@BusinessID", rating.BusinessID);

            conn.Open();
            rating.Company = Convert.ToString(cmd.ExecuteScalar());
            conn.Close();

            return View(rating);

        }


        public IActionResult SeeRating(RatingViewModel r)
        {


            return View(r);
        }








        [Authorize(Roles = "employeeuser")]
        //POST: UpdateEmployeeProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRating(RatingViewModel r)
        {
            try
            {

                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string query = "INSERT INTO dbo.Rating (OverallRating, WouldWorkAgain, SalaryRating, ManagementRating, FairnessRating, ClienteleRating, UnpaidTrialShift, Comments, DatePosted, IsDisplayed, EmployeeExperienceID, BusinessID, IsReported) OUTPUT Inserted.BusinessID VALUES (@OverallRating, @WouldWorkAgain, @SalaryRating, @ManagementRating, @FairnessRating, @ClienteleRating, @UnpaidTrialShift, @Comments, @DatePosted, @IsDisplayed, @EmployeeExperienceID, @BusinessID, @IsReported)";

                SqlCommand cmd = new SqlCommand(query, conn);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = query;


                cmd.Parameters.AddWithValue("@OverallRating", r.OverallRating);
                cmd.Parameters.AddWithValue("@WouldWorkAgain", r.WouldWorkAgain);
                cmd.Parameters.AddWithValue("@SalaryRating", r.SalaryRating);
                cmd.Parameters.AddWithValue("@ManagementRating", r.ManagementRating);
                cmd.Parameters.AddWithValue("@FairnessRating", r.FairnessRating);
                cmd.Parameters.AddWithValue("@ClienteleRating", r.ClienteleRating);
                cmd.Parameters.AddWithValue("@UnpaidTrialShift", r.UnpaidTrialShift);
                cmd.Parameters.AddWithValue("@Comments", r.Comments);
                cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsDisplayed", true);
                cmd.Parameters.AddWithValue("@EmployeeExperienceID", r.EmployeeExperienceID);
                cmd.Parameters.AddWithValue("@BusinessID", r.BusinessID);
                cmd.Parameters.AddWithValue("@IsReported", 0);


                conn.Open();
                var busID = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return View("SeeRating", r);
            }
            catch
            {
                return View("SeeRating", r);


            }
        }


        public IActionResult MyRatings(Rating r)
        {

            //get current user's ID to allow for all that user's jobs to be displayed
            var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            string query = "SELECT BusinessID FROM Business WHERE Business.LoginDetailsId = @LoginDetailsId";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);

            conn.Open();
            int currentbusinessId = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();



            connStr = _configuration.GetConnectionString("DefaultConnection");
            conn = new SqlConnection(connStr);


            query = "SELECT * FROM Rating WHERE Rating.BusinessID = @BusinessID AND IsDisplayed = 1";

            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BusinessID", currentbusinessId);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Rating> ratings_List = new List<Rating>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ratings_List.Add(new Rating
                    {
                        RatingID = Convert.ToInt32(rdr["RatingID"]),
                        OverallRating = Convert.ToInt32(rdr["OverallRating"]),
                        WouldWorkAgain = Convert.ToBoolean(rdr["WouldWorkAgain"]),
                        SalaryRating = Convert.ToInt32(rdr["SalaryRating"]),
                        ManagementRating = Convert.ToInt32(rdr["ManagementRating"]),
                        FairnessRating = Convert.ToInt32(rdr["FairnessRating"]),
                        ClienteleRating = Convert.ToInt32(rdr["ClienteleRating"]),
                        UnpaidTrialShift = Convert.ToBoolean(rdr["UnpaidTrialShift"]),
                        Comments = Convert.ToString(rdr["Comments"]),
                        DatePosted = Convert.ToDateTime(rdr["DatePosted"]),
                        IsDisplayed = Convert.ToBoolean(rdr["IsDisplayed"]),
                        EmployeeExperienceID = Convert.ToInt32(rdr["EmployeeExperienceID"]),
                        //BusinessID = Convert.ToInt32(rdr["BusinessID"]),
                    });
                }
            }

            conn.Close();


            return View(ratings_List);
        }









    }

}
