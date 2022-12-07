using HeardHospitality.Models;
using HeardHospitality.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.Data.SqlClient;

namespace HeardHospitality.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<LoginDetail> _userManager;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<LoginDetail> userManager)
        {
            _logger = logger;
            _configuration = config;
            _userManager = userManager;

        }

        public IActionResult Index()
        {


            return View();
        }

        //public IActionResult UpdateProfile(UpdateEmployeeProfileViewModel eup)
        //{
        //    return View(eup);
        //}

        //public IActionResult UpdateProfileSubmit(UpdateEmployeeProfileViewModel eup)
        //{


        //    return View(eup);
        //}


        //public IActionResult UpdateWorkHistory(EmployeeExperience ee)
        //{
        //    return View(ee);
        //}


        public IActionResult Businesses(Business b)
        {

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            string query = "SELECT * FROM Business LEFT OUTER JOIN Address on Business.BusinessID = Address.BusinessID";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            List<BusinessInfoViewModel> businesses_List = new List<BusinessInfoViewModel>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    businesses_List.Add(new BusinessInfoViewModel
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


            return View(businesses_List);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}