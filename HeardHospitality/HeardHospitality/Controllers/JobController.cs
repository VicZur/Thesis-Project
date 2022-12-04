using HeardHospitality.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Claims;

namespace HeardHospitality.Controllers
{
    public class JobController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<LoginDetail> _userManager;

        public JobController(IConfiguration config, UserManager<LoginDetail> userManager)
        {
            _configuration = config;
            _userManager = userManager;
        }

        public IActionResult Index(JobPostingViewModel j)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            //string query =  "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
            //                "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
            //                "INNER JOIN JobPerk on JobInfo.JobInfoID = JobPerk.JobInfoID " +
            //                "INNER JOIN Perk on JobPerk.PerkID = Perk.PerkID " +
            //                "WHERE JobInfo.IsActive = 1";

            string query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
                "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                "WHERE JobInfo.IsActive = 1";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            List<JobPostingViewModel> jobs_List = new List<JobPostingViewModel>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    jobs_List.Add(new JobPostingViewModel
                    {
                        BusinessID = Convert.ToInt32(rdr["BusinessId"]),
                        JobInfoID = Convert.ToInt32(rdr["JobInfoID"]),
                        //JobPerkID = Convert.ToInt32(rdr["JobPerkID"]),
                        //PerkID = Convert.ToInt32(rdr["PerkID"]),
                        BusinessName = Convert.ToString(rdr["BusinessName"]),
                        PhoneNum = Convert.ToString(rdr["PhoneNum"]),
                        AddressLine1 = Convert.ToString(rdr["AddressLine1"]),
                        AddressLine2 = Convert.ToString(rdr["AddressLine2"]),
                        City = Convert.ToString(rdr["City"]),
                        County = Convert.ToString(rdr["County"]),
                        EirCode = Convert.ToString(rdr["EirCode"]),
                        Country = Convert.ToString(rdr["Country"]),
                        Title = Convert.ToString(rdr["Title"]),
                        PositionType = Convert.ToString(rdr["PositionType"]),
                        Salary = Convert.ToDouble(rdr["Salary"]),
                        JobDescription = Convert.ToString(rdr["JobDescription"]),
                        PostedDate = Convert.ToDateTime(rdr["PostedDate"]),
                        MinExperience = Convert.ToString(rdr["MinExperience"]),
                        Category = Convert.ToString(rdr["Category"]),
                        Company = Convert.ToString(rdr["Company"]),
                        IsActive = Convert.ToBoolean(rdr["IsActive"]),
                        //PerkName = Convert.ToString(rdr["PerkName"]),
                        //Details = Convert.ToString(rdr["Details"]),
                    });
                }
            }

            conn.Close();



            return View(jobs_List);
        }


        public IActionResult AddJob(JobPostingViewModel j)
        {
            //Get current business ID to be able to link to new job posting
            var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            string query = "SELECT * FROM Business WHERE Business.LoginDetailsId = @LoginDetailsId";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);

            var jobposting = new JobPostingViewModel();
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                jobposting.BusinessID = Convert.ToInt32(rdr["BusinessID"]);
                jobposting.BusinessName = rdr["BusinessName"].ToString();
                jobposting.PhoneNum = rdr["PhoneNum"].ToString();

            }
            conn.Close();

            query = "SELECT * FROM Address WHERE Address.BusinessID = @BusinessID";
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@BusinessID", jobposting.BusinessID);

            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                jobposting.AddressLine1 = rdr["AddressLine1"].ToString();
                jobposting.AddressLine2 = rdr["AddressLine2"].ToString();
                jobposting.City = rdr["City"].ToString();
                jobposting.County = rdr["County"].ToString();
                jobposting.EirCode = rdr["EirCode"].ToString();
                jobposting.Country = rdr["Country"].ToString();
            }


            conn.Close();

            return View(jobposting);
        }




        public IActionResult SubmitJob(JobPostingViewModel j)
        {
            try
            {
                ////Get current business user ID
                //var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //string connStr = _configuration.GetConnectionString("DefaultConnection");
                //SqlConnection conn = new SqlConnection(connStr);
                //string query = "SELECT * FROM Business WHERE Business.LoginDetailsId = @LoginDetailsId";
                //SqlCommand cmd = new SqlCommand(query, conn);
                //cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);


                //conn.Open();
                //int currentbusID = Convert.ToInt32(cmd.ExecuteScalar());
                //conn.Close();


                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string query = "INSERT INTO dbo.JobInfo (Title, PositionType, Salary, JobDescription, PostedDate, MinExperience, Category, City, County, Company, IsActive, IsReported, BusinessID) " +
                                "VALUES (@Title, @PositionType, @Salary, @JobDescription, @PostedDate, @MinExperience, @Category, @City, @County, @Company, @IsActive, @IsReported, @BusinessID); " +
                                "INSERT INTO dbo.Perk (PerkName) VALUES (@PerkName);";

                SqlCommand cmd = new SqlCommand(query, conn);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = query;


                cmd.Parameters.AddWithValue("@Title", j.Title);
                cmd.Parameters.AddWithValue("@PositionType", j.PositionType);
                cmd.Parameters.AddWithValue("@Salary", j.Salary);
                cmd.Parameters.AddWithValue("@JobDescription", j.JobDescription);
                cmd.Parameters.AddWithValue("@PostedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@MinExperience", j.MinExperience);
                cmd.Parameters.AddWithValue("@Category", j.Category);
                cmd.Parameters.AddWithValue("@City", j.City);
                cmd.Parameters.AddWithValue("@County", j.County);
                cmd.Parameters.AddWithValue("@Company", j.Company);
                cmd.Parameters.AddWithValue("@IsActive", 1);
                cmd.Parameters.AddWithValue("@IsReported", 0);
                cmd.Parameters.AddWithValue("@BusinessID", j.BusinessID);
                cmd.Parameters.AddWithValue("@PerkName", j.PerkName);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("AddJob", "Job");

            }

            return RedirectToAction("AddJob", "Job");
        }
    }
}
