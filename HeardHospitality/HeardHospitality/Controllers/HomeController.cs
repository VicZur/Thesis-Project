using HeardHospitality.Models;
using HeardHospitality.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;

namespace HeardHospitality.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        { 
            _logger = logger;
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}