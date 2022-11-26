using HeardHospitality.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;
using HeardHospitality.Data;

namespace HeardHospitality.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<LoginDetail> _userManager;

        public BusinessController(IConfiguration config, UserManager<LoginDetail> userManager)
        {
            _configuration = config;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //POST: UpdateEmployeeProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Business b)
        {
            try
            {
                //get current user's ID to allow for update
                var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string query = "UPDATE Business SET BusinessName = @BusinessName, PhoneNum = @PhoneNum, LoginDetailsId = @LoginDetailsId " +
                    "WHERE Business.LoginDetailsId = @LoginDetailsId";

                SqlCommand cmd = new SqlCommand(query, conn);


                cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);
                cmd.Parameters.AddWithValue("@BusinessName", b.BusinessName);
                cmd.Parameters.AddWithValue("@PhoneNum", b.PhoneNum);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return RedirectToPage("Index");
            }
            catch
            {
                return RedirectToPage("Business");
            }
        }
    }
}
