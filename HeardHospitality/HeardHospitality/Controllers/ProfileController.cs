using HeardHospitality.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;
using HeardHospitality.Data;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace HeardHospitality.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<LoginDetail> _userManager;


        public ProfileController(IConfiguration config, UserManager<LoginDetail> userManager)
        {
            _configuration = config;
            _userManager = userManager;
        }


        //GET: UpdateEmployeeProfileController
        public IActionResult UpdateProfile(Employee e)
        {

            //get current user's ID to allow display data
            var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            string query = "SELECT * FROM LoginDetail WHERE LoginDetail.Id = @userID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@userID", currentuser);

            var user = new LoginDetail();
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                user.Id = currentuser;
                user.isBusinessAccount = Convert.ToBoolean(rdr["isBusinessAccount"]);
                user.isEmployeeAccount = Convert.ToBoolean(rdr["isEmployeeAccount"]);
            }
            conn.Close();


            if (user.isBusinessAccount == true)
            {
                return RedirectToAction("UpdateBusinessProfile", "Business", new { area = "" });
            }
            else
            {
                connStr = _configuration.GetConnectionString("DefaultConnection");
                conn = new SqlConnection(connStr);


                query = "SELECT * FROM Employee WHERE Employee.LoginDetailsId = @LoginDetailsId";

                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);

                var employee = new Employee();
                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    employee.FirstName = rdr["FirstName"].ToString();
                    employee.LastName = rdr["LastName"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.County = rdr["County"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Phone = rdr["Phone"].ToString();
                    employee.EmpBio = rdr["EmpBio"].ToString();
                    employee.DesiredJob = rdr["DesiredJob"].ToString();
                    employee.IsSearching = Convert.ToBoolean(rdr["IsSearching"]);
                    employee.IsVisible = Convert.ToBoolean(rdr["IsVisible"]);

                }
                conn.Close();

                //--------------------------------------------------------
                //with OG variable declarations - to use when writing seperate methods

                //string connStr = _configuration.GetConnectionString("DefaultConnection");
                //SqlConnection conn = new SqlConnection(connStr);


                //string query = "SELECT * FROM Employee WHERE Employee.LoginDetailsId = @LoginDetailsId";

                //SqlCommand cmd = new SqlCommand(query, conn);

                //cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);

                //var employee = new Employee();
                //conn.Open();
                //SqlDataReader rdr = cmd.ExecuteReader();

                //while (rdr.Read())
                //{

                //    employee.FirstName = rdr["FirstName"].ToString();
                //    employee.LastName = rdr["LastName"].ToString();
                //    employee.City = rdr["City"].ToString();
                //    employee.County = rdr["County"].ToString();
                //    employee.Gender = rdr["Gender"].ToString();
                //    employee.Phone = rdr["Phone"].ToString();
                //    employee.EmpBio = rdr["EmpBio"].ToString();
                //    employee.DesiredJob = rdr["DesiredJob"].ToString();
                //    employee.IsSearching = Convert.ToBoolean(rdr["IsSearching"]);
                //    employee.IsVisible = Convert.ToBoolean(rdr["IsVisible"]);

                //}
                //conn.Close();


                return View(employee);
            }
        }

        // POST: UpdateEmployeeProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee e)
        {
            try
            {

                //get current user's ID to allow for update
                var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, City = @City, County = @County, Gender = @Gender, Phone = @Phone, EmpBio = @EmpBio, DesiredJob = @DesiredJob, IsSearching = @IsSearching, IsVisible = @IsVisible, LoginDetailsId = @LoginDetailsId " +
                    "WHERE Employee.LoginDetailsId = @LoginDetailsId";
                //string query = "UPDATE Employee (FirstName, LastName, City, County, Gender, Phone, EmpBio, DesiredJob, IsSearching, IsVisible) " +
                //  "SET (FirstName = @FirstName, LastName = @LastName, City = @City, County = @County, Gender = @Gender, Phone = @Phone, EmpBip = @EmpBio, DesiredJob = @DesiredJob, IsSearching = @IsSearching, IsVisible = @IsVisible, LoginDetailsId = @LoginDetailsId) " +
                //  "WHERE Employee.LoginDetailsId = @LoginDetailsId";


                SqlCommand cmd = new SqlCommand(query, conn);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = query;


                cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);
                if (e.FirstName != null)
                {
                    cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                }

                if (e.LastName != null)
                {
                    cmd.Parameters.AddWithValue("@LastName", e.LastName);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                }

                if (e.City != null)
                {
                    cmd.Parameters.AddWithValue("@City", e.City);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@City", DBNull.Value);
                }

                if (e.County != null)
                {
                    cmd.Parameters.AddWithValue("@County", e.County);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@County", DBNull.Value);
                }

                if (e.Gender != null)
                {
                    cmd.Parameters.AddWithValue("@Gender", e.Gender);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Gender", DBNull.Value);
                }

                if (e.Phone != null)
                {
                    cmd.Parameters.AddWithValue("@Phone", e.Phone);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                }

                if (e.EmpBio != null)
                {
                    cmd.Parameters.AddWithValue("@EmpBio", e.EmpBio);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EmpBio", DBNull.Value);
                }

                if (e.DesiredJob != null)
                {
                    cmd.Parameters.AddWithValue("@DesiredJob", e.DesiredJob);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DesiredJob", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@IsSearching", e.IsSearching);
                cmd.Parameters.AddWithValue("@IsVisible", e.IsVisible);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("UpdateProfile");
            }
        }
        //        // POST: UpdateEmployeeProfileController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //        }

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

















        // GET: UpdateEmployeeProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UpdateEmployeeProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpdateEmployeeProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UpdateEmployeeProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }



        // GET: UpdateEmployeeProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UpdateEmployeeProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




        //public IActionResult AddEmployeeExperience(EmployeeExperience ee)
        //{
        //    return View(ee);
        //}

        ////POST: UpdateEmployeeProfileController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SubmitEmployeeExperience(EmployeeExperience ee)
        //{
        //    try
        //    {

        //        //get current user's ID to allow for update
        //        var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //        string connStr = _configuration.GetConnectionString("DefaultConnection");
        //        SqlConnection conn = new SqlConnection(connStr);

        //        string queryGetEmpID = "Select EmployeeID From Dbo.Employee where LoginDetailsId = @LoginDetailsID";

        //        SqlCommand command = new SqlCommand(queryGetEmpID, conn);

        //        command.Parameters.AddWithValue("@LoginDetailsID", currentuser);

        //        conn.Open();
        //        var currentEmpId = (Int32)command.ExecuteScalar();
        //        conn.Close();


        //        //string query = "UPDATE EmployeeExperience SET JobTitle = @JobTitle, Details = @Details, StartDate = @StartDate, EndDate = @EndDate, Company = @Company, City = @City, County = @County, IsVerified = 0, DisplayOnProfile = @DisplayOnProfile, EmployeeID = @EmployeeID " +
        //        //    "WHERE Employee.LoginDetailsId = @LoginDetailsId";
        //        string query = "INSERT INTO dbo.EmployeeExperience (JobTitle, Details, StartDate, EndDate, Company, City, County, IsVerified, DisplayOnProfile, EmployeeID) " +
        //          "VALUES (@JobTitle, @Details, @StartDate, @EndDate, @Company, @City, @County, @IsVerified, @DisplayOnProfile, @EmployeeID)";


        //        SqlCommand cmd = new SqlCommand(query, conn);

        //        //SqlCommand cmd = new SqlCommand();
        //        //cmd.Connection = conn;
        //        //cmd.CommandType = System.Data.CommandType.Text;
        //        //cmd.CommandText = query;


        //        cmd.Parameters.AddWithValue("@JobTitle", ee.JobTitle);
        //        cmd.Parameters.AddWithValue("@Details", ee.Details);
        //        cmd.Parameters.AddWithValue("@StartDate", ee.StartDate);
        //        cmd.Parameters.AddWithValue("@EndDate", ee.EndDate);
        //        cmd.Parameters.AddWithValue("@Company", ee.Company);
        //        cmd.Parameters.AddWithValue("@City", ee.City);
        //        cmd.Parameters.AddWithValue("@County", ee.County);
        //        cmd.Parameters.AddWithValue("@IsVerified", 0);
        //        cmd.Parameters.AddWithValue("@DisplayOnProfile", ee.DisplayOnProfile);
        //        cmd.Parameters.AddWithValue("@EmployeeID", currentEmpId);

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();

        //        return View(ee);
        //    }
        //    catch
        //    {
        //        return View(ee);
        //    }
        //}





    }
}
