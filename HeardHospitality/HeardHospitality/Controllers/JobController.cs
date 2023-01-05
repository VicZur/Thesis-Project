using HeardHospitality.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        public IActionResult FilterJob(string title, string company, string positiontype, double minsalary, string city, string county)
        {


            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            string query = "SELECT DISTINCT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
                "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                "WHERE (JobInfo.IsActive = 1) " +
                "AND (JobInfo.Salary >= @Salary) " +
                "AND (JobInfo.Title LIKE @Title) " +
                "AND (JobInfo.Company LIKE @Company) " +
                "AND (JobInfo.PositionType LIKE @PositionType) " +
                "AND (Address.City LIKE @City) " +
                "AND (Address.County LIKE @County) " +
                "ORDER BY PostedDate DESC";

            //        string query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
            //"INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
            //"WHERE (JobInfo.IsActive = 1) " +
            //"AND (JobInfo.Salary >= @Salary) " +
            //"AND (@Title IS NULL OR JobInfo.Title LIKE @Title) " +
            //"AND (@Company IS NULL OR JobInfo.Company LIKE @Company) " +
            //"AND (@PositionType IS NULL OR JobInfo.PositionType LIKE @PositionType) " +
            //"AND (@City IS NULL OR Address.City LIKE @City) " +
            //"AND (@County IS NULL OR Address.County LIKE @County) " +
            //"ORDER BY PostedDate DESC";

            SqlCommand cmd = new SqlCommand(query, conn);

            if (title == null)
            {
                cmd.Parameters.AddWithValue("@Title", "%");

            }
            else cmd.Parameters.AddWithValue("@Title", "%" + title + "%");

            if (company == null)
            {
                cmd.Parameters.AddWithValue("@Company", "%");

            }
            else cmd.Parameters.AddWithValue("@Company", "%" + company + "%");
            if (positiontype == null || positiontype == "all")
            {
                cmd.Parameters.AddWithValue("@PositionType", "%");

            }
            else if (positiontype == "fulltime")
            {
                cmd.Parameters.AddWithValue("@PositionType", "%" + "Full Time" + "%");
            }
            else cmd.Parameters.AddWithValue("@PositionType", "%" + "Part Time" + "%");

            cmd.Parameters.AddWithValue("@Salary", minsalary);

            if (city == null)
            {
                cmd.Parameters.AddWithValue("@City", "%");

            }
            else cmd.Parameters.AddWithValue("@City", "%" + city + "%");

            if (county == null)
            {
                cmd.Parameters.AddWithValue("@County", "%");
            }
            else cmd.Parameters.AddWithValue("@County", "%" + county + "%");

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
                    });
                }
            }
            conn.Close();


            return PartialView("_JobsPartial", jobs_List);

        }


        //public IActionResult FilterJob(string title, string company, string positionType, double? salary, string city, string county, List<JobPostingViewModel> originalList)
        //{

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        originalList = originalList.Where(jp => jp.Title.Contains(title)).ToList();
        //    }
        //    if (positionType != "all")
        //    {
        //        originalList = originalList.Where(jp => jp.PositionType == positionType).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(company))
        //    {
        //        originalList = originalList.Where(jp => jp.Company.Contains(company)).ToList();
        //    }
        //    if (salary.HasValue)
        //    {
        //        originalList = originalList.Where(jp => jp.Salary >= salary).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(city))
        //    {
        //        originalList = originalList.Where(jp => jp.City.Contains(city)).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(county))
        //    {
        //        originalList = originalList.Where(jp => jp.County.Contains(county)).ToList();
        //    }

        //    int count = originalList.Count;

        //    return View("SearchJob", originalList);

        //}





        public IActionResult SearchJob(JobPostingViewModel j)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            //string query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
            //    "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
            //    "WHERE (JobInfo.IsActive = 1) " +
            //    "AND (@Salary IS NULL OR @Salary = '0' OR JobInfo.Salary LIKE @Salary) " +
            //    "AND (@Title IS NULL OR JobInfo.Title LIKE @Title) " +
            //    "AND (@Company IS NULL OR JobInfo.Company LIKE @Company) " +
            //    "AND (@PositionType IS NULL OR JobInfo.PositionType LIKE @PositionType) " +
            //    "AND (@City IS NULL OR Address.City LIKE @City) " +
            //    "AND (@County IS NULL OR Address.County LIKE @County) " +
            //    "ORDER BY PostedDate DESC";


            string query = "SELECT DISTINCT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
                           "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                           "WHERE (JobInfo.IsActive = 1) " +
                           "AND (JobInfo.Title LIKE @Title OR JobInfo.Company LIKE @Company OR Business.BusinessName LIKE @Company OR JobInfo.PositionType LIKE @PositionType OR Address.City LIKE @City OR Address.County LIKE @County) " +
                           "ORDER BY PostedDate DESC";


            SqlCommand cmd = new SqlCommand(query, conn);

            //cmd.Parameters.AddWithValue("@Title", "%" + j.Title + "%");
            //cmd.Parameters.AddWithValue("@Salary", j.Salary);
            //cmd.Parameters.AddWithValue("@PositionType", "%" + j.PositionType + "%");
            //cmd.Parameters.AddWithValue("@City", "%" + j.City + "%");
            //cmd.Parameters.AddWithValue("@County", "%" + j.County + "%");
            //cmd.Parameters.AddWithValue("@Company", "%" + j.Company + "%");


            cmd.Parameters.AddWithValue("@Title", "%" + j.JobDescription + "%");
            //cmd.Parameters.AddWithValue("@Salary", j.JobDescription);
            cmd.Parameters.AddWithValue("@PositionType", "%" + j.JobDescription + "%");
            cmd.Parameters.AddWithValue("@City", "%" + j.JobDescription + "%");
            cmd.Parameters.AddWithValue("@County", "%" + j.JobDescription + "%");
            cmd.Parameters.AddWithValue("@Company", "%" + j.JobDescription + "%");

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



            // return RedirectToAction("Index");
            // return RedirectToAction("Index", new { list = jobs_List });
            return View(jobs_List);
        }





        public IActionResult Index(JobPostingViewModel j)
        {


            // if (jobs_List == null)
            // {
            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            //string query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
            //                "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
            //                "INNER JOIN JobPerk on JobInfo.JobInfoID = JobPerk.JobInfoID " +
            //                "INNER JOIN Perk on JobPerk.PerkID = Perk.PerkID " +
            //                "WHERE JobInfo.IsActive = 1";

            string query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
                "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                //"INNER JOIN JobPerk on JobInfo.JobInfoID = JobPerk.JobInfoID " +
                //"INNER JOIN Perk on JobPerk.PerkID = Perk.PerkID " +
                "WHERE JobInfo.IsActive = 1 " +
                "ORDER BY PostedDate DESC";

            //string query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
            //    "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
            //    "WHERE JobInfo.IsActive = 1";

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

            // }


            return View(jobs_List);
        }

        [Authorize(Roles = "businessuser")]
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


            jobposting.Perks = new List<PerkViewModel>();


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

        //public void addPerk()
        //{
        //    model.Perks.Add(new PerkViewModel());
        //}



        //[Authorize(Roles = "businessuser")]
        //public IActionResult SubmitJob(JobPostingViewModel j)
        //{
        //    try
        //    {
        //        ////Get current business user ID
        //        //var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        //string connStr = _configuration.GetConnectionString("DefaultConnection");
        //        //SqlConnection conn = new SqlConnection(connStr);
        //        //string query = "SELECT * FROM Business WHERE Business.LoginDetailsId = @LoginDetailsId";
        //        //SqlCommand cmd = new SqlCommand(query, conn);
        //        //cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);


        //        //conn.Open();
        //        //int currentbusID = Convert.ToInt32(cmd.ExecuteScalar());
        //        //conn.Close();


        //        string connStr = _configuration.GetConnectionString("DefaultConnection");
        //        SqlConnection conn = new SqlConnection(connStr);

        //        string query = "INSERT INTO dbo.JobInfo (Title, PositionType, Salary, JobDescription, PostedDate, MinExperience, Category, City, County, Company, IsActive, IsReported, BusinessID) " +
        //                        "VALUES (@Title, @PositionType, @Salary, @JobDescription, @PostedDate, @MinExperience, @Category, @City, @County, @Company, @IsActive, @IsReported, @BusinessID); " +
        //                        "INSERT INTO dbo.Perk (PerkName) VALUES (@PerkName);";

        //        SqlCommand cmd = new SqlCommand(query, conn);

        //        //SqlCommand cmd = new SqlCommand();
        //        //cmd.Connection = conn;
        //        //cmd.CommandType = System.Data.CommandType.Text;
        //        //cmd.CommandText = query;


        //        cmd.Parameters.AddWithValue("@Title", j.Title);
        //        cmd.Parameters.AddWithValue("@PositionType", j.PositionType);
        //        cmd.Parameters.AddWithValue("@Salary", j.Salary);
        //        cmd.Parameters.AddWithValue("@JobDescription", j.JobDescription);
        //        cmd.Parameters.AddWithValue("@PostedDate", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@MinExperience", j.MinExperience);
        //        cmd.Parameters.AddWithValue("@Category", j.Category);
        //        cmd.Parameters.AddWithValue("@City", j.City);
        //        cmd.Parameters.AddWithValue("@County", j.County);
        //        cmd.Parameters.AddWithValue("@Company", j.Company);
        //        cmd.Parameters.AddWithValue("@IsActive", 1);
        //        cmd.Parameters.AddWithValue("@IsReported", 0);
        //        cmd.Parameters.AddWithValue("@BusinessID", j.BusinessID);
        //        cmd.Parameters.AddWithValue("@PerkName", j.PerkName);


        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();

        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("AddJob", "Job");

        //    }

        //    return RedirectToAction("AddJob", "Job");
        //}

        [Authorize(Roles = "businessuser")]
        public IActionResult SubmitJob(JobPostingViewModel j)
        {
            try
            {
                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();

                if (j.Perks != null)
                {
                    foreach (var perk in j.Perks)
                    {
                        if (perk.PerkName != null)
                        {
                            cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "uspAddPerkIfNotExists"; //This sproc checks to see if the perk already exists in the DB, if it does NOT, the sproc will add it

                            cmd.Parameters.AddWithValue("@PerkName", perk.PerkName);
                            conn.Open();
                            object o = cmd.ExecuteScalar();
                            conn.Close();
                        }
                    }

                }

                //string connStr = _configuration.GetConnectionString("DefaultConnection");
                //SqlConnection conn = new SqlConnection(connStr);

                //Reference for calling sproc
                //https://stackoverflow.com/questions/39587606/how-to-call-and-execute-stored-procedures-in-asp-net-mvcc

                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "uspAddJob";

                cmd.Parameters.Clear();
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
                conn.Open();
                var newJobID = cmd.ExecuteScalar();
                conn.Close();


                if (j.Perks != null)
                {
                    foreach (var perk in j.Perks)
                    {
                        if (perk.PerkName != null)
                        {
                            cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "uspAddJobPerk"; //This sproc adds new job perks 

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@JobInfoId", newJobID);
                            cmd.Parameters.AddWithValue("@PerkName", perk.PerkName);
                            if (perk.Details != null)
                            {
                                cmd.Parameters.AddWithValue("@Details", perk.Details);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Details", DBNull.Value);
                            }

                            conn.Open();
                            object o = cmd.ExecuteScalar();
                            conn.Close();
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("AddJob", "Job");

            }

            return RedirectToAction("ViewBusinessJobs");
        }

        public IActionResult JobDetails(JobPostingViewModel j, int jobID)
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
                "WHERE JobInfo.IsActive = 1 " +
                "AND JobInfoID = @JobID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@JobID", jobID);


            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            JobPostingViewModel jobPosting = new JobPostingViewModel();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    jobPosting.BusinessID = Convert.ToInt32(rdr["BusinessId"]);
                    jobPosting.JobInfoID = Convert.ToInt32(rdr["JobInfoID"]);
                    //jobPosting.JobPerkID = Convert.ToInt32(rdr["JobPerkID"]);
                    //jobPosting.PerkID = Convert.ToInt32(rdr["PerkID"]);
                    jobPosting.BusinessName = Convert.ToString(rdr["BusinessName"]);
                    jobPosting.PhoneNum = Convert.ToString(rdr["PhoneNum"]);
                    jobPosting.AddressLine1 = Convert.ToString(rdr["AddressLine1"]);
                    jobPosting.AddressLine2 = Convert.ToString(rdr["AddressLine2"]);
                    jobPosting.City = Convert.ToString(rdr["City"]);
                    jobPosting.County = Convert.ToString(rdr["County"]);
                    jobPosting.EirCode = Convert.ToString(rdr["EirCode"]);
                    jobPosting.Country = Convert.ToString(rdr["Country"]);
                    jobPosting.Title = Convert.ToString(rdr["Title"]);
                    jobPosting.PositionType = Convert.ToString(rdr["PositionType"]);
                    jobPosting.Salary = Convert.ToDouble(rdr["Salary"]);
                    jobPosting.JobDescription = Convert.ToString(rdr["JobDescription"]);
                    jobPosting.PostedDate = Convert.ToDateTime(rdr["PostedDate"]);
                    jobPosting.MinExperience = Convert.ToString(rdr["MinExperience"]);
                    jobPosting.Category = Convert.ToString(rdr["Category"]);
                    jobPosting.Company = Convert.ToString(rdr["Company"]);
                    jobPosting.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    //jobPosting.PerkName = Convert.ToString(rdr["PerkName"]);
                    //jobPosting.Details = Convert.ToString(rdr["Details"]);
                }
            }
            conn.Close();


            query = "SELECT * FROM dbo.JobPerk INNER JOIN Perk on Perk.PerkID = JobPerk.PerkID WHERE JobInfoID = @JobInfoID";

            cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@JobInfoID", jobID);

            conn.Open();
            rdr = cmd.ExecuteReader();

            List<PerkViewModel> perks = new List<PerkViewModel>();

            if (rdr.HasRows)
            {

                while (rdr.Read())
                {

                    PerkViewModel perk = new PerkViewModel();
                    perk.PerkName = Convert.ToString(rdr["PerkName"]);
                    perk.Details = Convert.ToString(rdr["Details"]);
                    perks.Add(perk);

                    //jobPosting.JobPerkID = Convert.ToInt32(rdr["JobPerkID"]);
                    //jobPosting.PerkID = Convert.ToInt32(rdr["PerkID"]);
                    //jobPosting.PerkName = Convert.ToString(rdr["PerkName"]);
                    //jobPosting.Details = Convert.ToString(rdr["Details"]);
                }
            }

            jobPosting.Perks = perks;

            conn.Close();

            return View(jobPosting);
        }

        //public IActionResult ReportJob(JobPostingViewModel j, int jobID)
        //{
        //    try
        //    {
        //        string connStr = _configuration.GetConnectionString("DefaultConnection");
        //        SqlConnection conn = new SqlConnection(connStr);

        //        string query = "UPDATE JobInfo SET IsReported = @IsReported " +
        //            "WHERE JobInfo.JobInfoID = @JobID";

        //        SqlCommand cmd = new SqlCommand(query, conn);

        //        cmd.Parameters.AddWithValue("@JobId", jobID);
        //        cmd.Parameters.AddWithValue("@IsReported", 1);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();

        //        return RedirectToAction("ReportJob", new { jobID = jobID });
        //    }
        //    catch
        //    {
        //        return RedirectToAction("JobDetails", new { jobID = jobID });
        //    }
        //}

        public IActionResult ReportJob(ReportedJobDetail rjd, int jobID)
        {
            //get current user id (employee id) to be used as a Fk reference
            var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            string queryGetEmpID = "Select EmployeeID From Dbo.Employee where LoginDetailsId = @LoginDetailsID";

            SqlCommand command = new SqlCommand(queryGetEmpID, conn);

            command.Parameters.AddWithValue("@LoginDetailsID", currentuser);

            conn.Open();
            var currentEmpId = (Int32)command.ExecuteScalar();
            conn.Close();


            ReportedJobDetail reportedJob = new ReportedJobDetail();

            reportedJob.AdDetailsIncorrect = false;
            reportedJob.PerksListedIncorrect = false;
            reportedJob.SalaryIncorrect = false;
            reportedJob.IllegalExpectations = false;
            reportedJob.Comments = "";
            reportedJob.JobInfoID = jobID;
            reportedJob.EmployeeID = currentEmpId;


            return View(reportedJob);
        }

        public IActionResult SubmitReporting(ReportedJobDetail rjd)
        {
            try
            {
                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                //Check to make sure the user has not already reported this job before - to ensure can only report once
                string query = "SELECT CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) FROM ReportedJobDetail WHERE EmployeeID = @EmployeeID; ";

                SqlCommand cmd = new SqlCommand(query, conn);


                cmd.Parameters.AddWithValue("@EmployeeID", rjd.EmployeeID);

                conn.Open();
                bool alreadyReported = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();



                if (alreadyReported == false)
                {
                    query = "UPDATE JobInfo SET IsReported = @IsReported " +
                    "WHERE JobInfo.JobInfoID = @JobID";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@JobId", rjd.JobInfoID);
                    cmd.Parameters.AddWithValue("@IsReported", 1);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    query = "INSERT INTO dbo.ReportedJobDetail (AdDetailsIncorrect, PerksListedIncorrect, SalaryIncorrect, IllegalExpectations, Comments, JobInfoID, EmployeeID) " +
                            "Values (@AdDetailsIncorrect, @PerksListedIncorrect, @SalaryIncorrect, @IllegalExpectations, @Comments, @JobInfoID, @EmployeeID)";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AdDetailsIncorrect", rjd.AdDetailsIncorrect);
                    cmd.Parameters.AddWithValue("@PerksListedIncorrect", rjd.PerksListedIncorrect);
                    cmd.Parameters.AddWithValue("@SalaryIncorrect", rjd.SalaryIncorrect);
                    cmd.Parameters.AddWithValue("@IllegalExpectations", rjd.IllegalExpectations);
                    cmd.Parameters.AddWithValue("@Comments", rjd.Comments);
                    cmd.Parameters.AddWithValue("@JobInfoID", rjd.JobInfoID);
                    cmd.Parameters.AddWithValue("@EmployeeID", rjd.EmployeeID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    return RedirectToAction("ReportedSuccessfully");
                }
                else
                {
                    return RedirectToAction("ReportedUnSuccessfully");
                }

            }
            catch
            {
                return RedirectToAction("JobDetails", new { jobID = rjd.JobInfoID });
            }

        }



        public IActionResult ReportedSuccessfully()
        {
            return View();
        }

        public IActionResult ReportedUnSuccessfully()
        {
            return View();
        }

        public IActionResult ViewBusinessJobs(JobPostingViewModel j)
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

            query = "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
                "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                "WHERE JobInfo.IsActive = 1 AND JobInfo.BusinessID = @BusinessID";

            cmd = new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue("@BusinessID", currentbusinessId);


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


        public IActionResult Delete(JobPostingViewModel j, int jobID)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);

            //string query = "DELETE FROM dbo.ReportedJobDetail WHERE JobInfoID = @JobInfoID; " +
            //                "DELETE FROM dbo.JobPerk WHERE jobInfoID = @JobInfoID; " +
            //                "DELETE FROM dbo.JobInfo WHERE JobInfoID = @JobInfoID";

            string query = "UPDATE JobInfo SET isActive = 0 WHERE JobInfoID = @JobInfoID";



            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@JobInfoID", jobID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("ViewBusinessJobs");
        }
    }
}

