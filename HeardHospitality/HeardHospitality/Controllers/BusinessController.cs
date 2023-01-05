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

        //GET: UpdateBusinessProfileController
        public IActionResult UpdateBusinessProfile(BusinessInfoViewModel bi)
        {
            //get current user's ID to allow display data
            var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string connStr = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);


            string query = "SELECT * FROM Business WHERE Business.LoginDetailsId = @LoginDetailsId";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);

            var businessinfo = new BusinessInfoViewModel();
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                businessinfo.BusinessID = Convert.ToInt32(rdr["BusinessID"]);
                businessinfo.BusinessName = rdr["BusinessName"].ToString();
                businessinfo.PhoneNum = rdr["PhoneNum"].ToString();

            }
            conn.Close();

            query = "SELECT * FROM Address WHERE Address.BusinessID = @BusinessID";
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@BusinessID", businessinfo.BusinessID);

            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                businessinfo.AddressLine1 = rdr["AddressLine1"].ToString();
                businessinfo.AddressLine2 = rdr["AddressLine2"].ToString();
                businessinfo.City = rdr["City"].ToString();
                businessinfo.County = rdr["County"].ToString();
                businessinfo.EirCode = rdr["EirCode"].ToString();
                businessinfo.Country = rdr["Country"].ToString();
            }

            return View(businessinfo);
        }

        //POST: UpdateBusinessProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusinessInfoViewModel bi)
        {
            try
            {
                //get current user's ID to allow for update
                var currentuser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                string connStr = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connStr);

                string query = "UPDATE Business SET BusinessName = @BusinessName, PhoneNum = @PhoneNum " +
                    "OUTPUT inserted.BusinessID WHERE Business.LoginDetailsId = @LoginDetailsId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@LoginDetailsId", currentuser);
                cmd.Parameters.AddWithValue("@BusinessName", bi.BusinessName);
                cmd.Parameters.AddWithValue("@PhoneNum", bi.PhoneNum);

                conn.Open();
                int currentbusinessId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                query = "UPDATE Address SET AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, County = @County, EirCode = @EirCode, Country = @Country " +
                    "WHERE Address.BusinessId = @BusinessId";

                 cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@BusinessId", currentbusinessId);
                cmd.Parameters.AddWithValue("@AddressLine1", bi.AddressLine1);
                if (bi.AddressLine2 != null)
                {
                    cmd.Parameters.AddWithValue("@AddressLine2", bi.AddressLine2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AddressLine2", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@City", bi.City);
                cmd.Parameters.AddWithValue("@County", bi.County);
                if (bi.EirCode != null)
                {
                    cmd.Parameters.AddWithValue("@EirCode", bi.EirCode);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EirCode", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@Country", bi.Country);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("UpdateBusinessProfile");
            }
        }



        


    }
}
