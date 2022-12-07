using HeardHospitality.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
                        PositionType = Convert.ToString(rdr["PositionType"]),
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


                //string query = "UPDATE EmployeeExperience SET JobTitle = @JobTitle, PositionType = @PositionType, StartDate = @StartDate, EndDate = @EndDate, Company = @Company, City = @City, County = @County, IsVerified = 0, DisplayOnProfile = @DisplayOnProfile, EmployeeID = @EmployeeID " +
                //    "WHERE Employee.LoginDetailsId = @LoginDetailsId";
                string query = "INSERT INTO dbo.EmployeeExperience (JobTitle, PositionType, StartDate, EndDate, Company, City, County, IsVerified, DisplayOnProfile, EmployeeID) " +
                  "VALUES (@JobTitle, @PositionType, @StartDate, @EndDate, @Company, @City, @County, @IsVerified, @DisplayOnProfile, @EmployeeID)";


                SqlCommand cmd = new SqlCommand(query, conn);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = query;


                cmd.Parameters.AddWithValue("@JobTitle", ee.JobTitle);
                cmd.Parameters.AddWithValue("@PositionType", ee.PositionType);
                cmd.Parameters.AddWithValue("@StartDate", ee.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", ee.EndDate);
                cmd.Parameters.AddWithValue("@Company", ee.Company);
                cmd.Parameters.AddWithValue("@City", ee.City);
                cmd.Parameters.AddWithValue("@County", ee.County);
                cmd.Parameters.AddWithValue("@IsVerified", 0);
                cmd.Parameters.AddWithValue("@DisplayOnProfile", ee.DisplayOnProfile);
                cmd.Parameters.AddWithValue("@EmployeeID", currentEmpId);

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
