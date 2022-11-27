using HeardHospitality.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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


            string query = "SELECT * FROM Rating WHERE Rating.BusinessID = @BusinessID AND IsDisplayed = 1";

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
                        BusinessID = Convert.ToInt32(rdr["BusinessID"]),
                    });
                }
            }

            conn.Close();


            return View(ratings_List);
        }


        public IActionResult New(Rating r, int busID)
        {
            var rating = new Rating { BusinessID = busID };

            return View(rating);
        }


        //POST: UpdateEmployeeProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRating(Rating r)
        {
            try
            {

                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string query = "INSERT INTO dbo.Rating (OverallRating, WouldWorkAgain, SalaryRating, ManagementRating, FairnessRating, ClienteleRating, UnpaidTrialShift, Comments, DatePosted, IsDisplayed, EmployeeExperienceID, BusinessID) OUTPUT Inserted.BusinessID VALUES (@OverallRating, @WouldWorkAgain, @SalaryRating, @ManagementRating, @FairnessRating, @ClienteleRating, @UnpaidTrialShift, @Comments, @DatePosted, @IsDisplayed, @EmployeeExperienceID, @BusinessID)";

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
                cmd.Parameters.AddWithValue("@EmployeeExperienceID", 1);
                cmd.Parameters.AddWithValue("@BusinessID", r.BusinessID);


                conn.Open();
                var busID = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return RedirectToAction("All", "Rating", new { busID = busID });
            }
            catch
            {
                return RedirectToAction("New", "Rating", new { busID = r.BusinessID });

            }
        }












    }

}
