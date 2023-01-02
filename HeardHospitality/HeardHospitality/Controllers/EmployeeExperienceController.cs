using HeardHospitality.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace HeardHospitality.Controllers
{
    public class EmployeeExperienceController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<LoginDetail> _userManager;




        public EmployeeExperienceController(IConfiguration config, UserManager<LoginDetail> userManager)
        {
            _configuration = config;
            _userManager = userManager;
        }

        public IActionResult ViewEmployeeExperience(EmployeeExperience ee)
        {
            //get current user's ID to allow for update
            var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            string queryGetEmpID = "Select EmployeeID From Dbo.Employee where LoginDetailsId = @LoginDetailsID";

            SqlCommand command = new SqlCommand(queryGetEmpID, conn);

            command.Parameters.AddWithValue("@LoginDetailsID", currentuser);

            conn.Open();
            var currentEmpId = (Int32)command.ExecuteScalar();
            conn.Close();


            string query = "Select * From Dbo.EmployeeExperience WHERE EmployeeID = @EmployeeID";

            command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@EmployeeID", currentEmpId);

            conn.Open();
            SqlDataReader rdr = command.ExecuteReader();

            List<EmployeeExperience> experience_List = new List<EmployeeExperience>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    experience_List.Add(new EmployeeExperience
                    {
                        EmployeeExperienceID = Convert.ToInt32(rdr["EmployeeExperienceID"]),
                        JobTitle = Convert.ToString(rdr["JobTitle"]),
                        Details = Convert.ToString(rdr["Details"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        EndDate = Convert.ToDateTime(rdr["EndDate"]),
                        Company = Convert.ToString(rdr["Company"]),
                        City = Convert.ToString(rdr["City"]),
                        County = Convert.ToString(rdr["County"]),
                        IsVerified = Convert.ToBoolean(rdr["IsVerified"]),
                        DisplayOnProfile = Convert.ToBoolean(rdr["DisplayOnProfile"]),
                        EmployeeID = Convert.ToInt32(rdr["EmployeeID"]),
                    });
                }
            }

            conn.Close();


            return View(experience_List);
        }



        public IActionResult AddEmployeeExperience(EmployeeExperience ee)
        {
            return View(ee);
        }

        public IActionResult ShowpopUp()
        {
            //specify the name or path of the partial view
            return PartialView("_BusinessInfoPartial");
        }


        [HttpGet]
        public ActionResult BusinssInfo(string company, string city, string county)
        {

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            string query = "SELECT * FROM Business INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                           "WHERE (Business.BusinessName LIKE @Company AND Address.City LIKE @City And Address.County LIKE @County)";

            SqlCommand cmd = new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue("@Company", "%" + company + "%");
            cmd.Parameters.AddWithValue("@City", "%" + city + "%");
            cmd.Parameters.AddWithValue("@County", "%" + county + "%");

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            List<BusinessInfoViewModel> business_List = new List<BusinessInfoViewModel>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    business_List.Add(new BusinessInfoViewModel
                    {
                        BusinessID = Convert.ToInt32(rdr["BusinessId"]),
                        BusinessName = Convert.ToString(rdr["BusinessName"]),
                        PhoneNum = Convert.ToString(rdr["PhoneNum"]),
                        AddressLine1 = Convert.ToString(rdr["AddressLine1"]),
                        AddressLine2 = Convert.ToString(rdr["AddressLine2"]),
                        City = Convert.ToString(rdr["City"]),
                        County = Convert.ToString(rdr["County"]),
                        EirCode = Convert.ToString(rdr["EirCode"]),
                        Country = Convert.ToString(rdr["Country"]),
                    });
                }
            }
            conn.Close();

            return PartialView("_BusinessInfoPartial", business_List);
            //return PartialView("_BusinessInfoPartial");

        }

        //POST: UpdateEmployeeProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitEmployeeExperience(EmployeeExperience ee)
        {
            try
            {

                //get current user's ID to allow for update
                var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string queryGetEmpID = "Select EmployeeID From Dbo.Employee where LoginDetailsId = @LoginDetailsID";

                SqlCommand command = new SqlCommand(queryGetEmpID, conn);

                command.Parameters.AddWithValue("@LoginDetailsID", currentuser);

                conn.Open();
                var currentEmpId = (Int32)command.ExecuteScalar();
                conn.Close();

                if (ee.BusinessID == null)
                {
                    string aquery = "INSERT INTO dbo.Business (BusinessName, IsVerified) " +
                                    "OUTPUT inserted.BusinessID " +
                                    "VALUES (@BusinessName, @IsVerified)";

                    SqlCommand acmd = new SqlCommand(aquery, conn);

                    acmd.Parameters.AddWithValue("@BusinessName", ee.Company);
                    acmd.Parameters.AddWithValue("@IsVerified", 0);

                    conn.Open();
                    ee.BusinessID = (Int32)acmd.ExecuteScalar();
                    conn.Close();


                    aquery = "INSERT INTO dbo.Address (City, County, Country, BusinessID) " +
                                    "VALUES (@City, @County, @Country, @BusinessID)";

                    acmd = new SqlCommand(aquery, conn);

                    acmd.Parameters.Clear();
                    acmd.Parameters.AddWithValue("@City", ee.City);
                    acmd.Parameters.AddWithValue("@County", ee.County);
                    acmd.Parameters.AddWithValue("@Country", "Ireland");
                    acmd.Parameters.AddWithValue("@BusinessID", ee.BusinessID);

                    conn.Open();
                    acmd.ExecuteNonQuery();
                    conn.Close();


                }
                //string query = "UPDATE EmployeeExperience SET JobTitle = @JobTitle, Details = @Details, StartDate = @StartDate, EndDate = @EndDate, Company = @Company, City = @City, County = @County, IsVerified = 0, DisplayOnProfile = @DisplayOnProfile, EmployeeID = @EmployeeID " +
                //    "WHERE Employee.LoginDetailsId = @LoginDetailsId";
                string query = "INSERT INTO dbo.EmployeeExperience (JobTitle, Details, StartDate, EndDate, Company, City, County, IsVerified, DisplayOnProfile, EmployeeID, BusinessID) " +
                  "VALUES (@JobTitle, @Details, @StartDate, @EndDate, @Company, @City, @County, @IsVerified, @DisplayOnProfile, @EmployeeID, @BusinessID)";


                SqlCommand cmd = new SqlCommand(query, conn);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = query;


                cmd.Parameters.AddWithValue("@JobTitle", ee.JobTitle);
                cmd.Parameters.AddWithValue("@Details", ee.Details);
                cmd.Parameters.AddWithValue("@StartDate", ee.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", ee.EndDate);
                cmd.Parameters.AddWithValue("@Company", ee.Company);
                cmd.Parameters.AddWithValue("@City", ee.City);
                cmd.Parameters.AddWithValue("@County", ee.County);
                cmd.Parameters.AddWithValue("@IsVerified", 0);
                cmd.Parameters.AddWithValue("@DisplayOnProfile", ee.DisplayOnProfile);
                cmd.Parameters.AddWithValue("@EmployeeID", currentEmpId);
                cmd.Parameters.AddWithValue("@BusinessID", ee.BusinessID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return View(ee);






            }
            catch
            {
                return View(ee);
            }
        }
    }
}
