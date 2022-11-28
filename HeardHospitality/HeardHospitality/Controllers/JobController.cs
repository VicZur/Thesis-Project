using HeardHospitality.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

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


            string query =  "SELECT * FROM JobInfo INNER JOIN Business on JobInfo.BusinessID = Business.BusinessID " +
                            "INNER JOIN Address on Business.BusinessID = Address.BusinessID " +
                            "INNER JOIN JobPerk on JobInfo.JobInfoID = JobPerk.JobInfoID " +
                            "INNER JOIN Perk on JobPerk.PerkID = Perk.PerkID " +
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
                        JobPerkID = Convert.ToInt32(rdr["JobPerkID"]),
                        PerkID = Convert.ToInt32(rdr["PerkID"]),
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
                        PerkName = Convert.ToString(rdr["PerkName"]),
                        Details = Convert.ToString(rdr["Details"]),
                    });
                }
            }

            conn.Close();


            return View(jobs_List);
        }
    }
}
