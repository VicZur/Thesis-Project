using HeardHospitality.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace HeardHospitality.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult EmployeeSignUp(EmployeeSignUpViewModel esuvm)
        {
            return View(esuvm);
        }

        public IActionResult EmployeeSignUpSubmit(EmployeeSignUpViewModel esus)
        {
            //https://www.youtube.com/watch?v=zZ5KvYKgMxI&ab_channel=GenGrievous
            //https://www.youtube.com/watch?v=OwH6PnXYtL8&list=PLN_YHff_frgy6t4HEpa1YiTHxeka1HazX&index=9&ab_channel=GenGrievous
            //Connect to DB
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlConnection conn = new SqlConnection(connStr);

            //call sproc to add new business using info provided on view form
            //Reference for calling sproc
            //https://stackoverflow.com/questions/39587606/how-to-call-and-execute-stored-procedures-in-asp-net-mvcc
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "uspNewEmployee";

            cmd.Parameters.AddWithValue("@email", esus.Email);
            cmd.Parameters.AddWithValue("@username", esus.Username);
            cmd.Parameters.AddWithValue("@userpassword", esus.UserPassword);


            conn.Open();
            object o = cmd.ExecuteScalar();
            conn.Close();


            return View(esus);
        }




    public IActionResult BusinessSignUp(BusinessSignUpViewModel bsuvm)
        {
            return View(bsuvm);
        }

            public IActionResult BusinessSignUpSubmit(BusinessSignUpViewModel bsus)
        {
            //https://www.youtube.com/watch?v=zZ5KvYKgMxI&ab_channel=GenGrievous
            //https://www.youtube.com/watch?v=OwH6PnXYtL8&list=PLN_YHff_frgy6t4HEpa1YiTHxeka1HazX&index=9&ab_channel=GenGrievous
            //Connect to DB
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlConnection conn = new SqlConnection(connStr);

            //call sproc to add new business using info provided on view form
            //Reference for calling sproc
            //https://stackoverflow.com/questions/39587606/how-to-call-and-execute-stored-procedures-in-asp-net-mvcc
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "uspNewBusiness";

            cmd.Parameters.AddWithValue("@email", bsus.Email);
            cmd.Parameters.AddWithValue("@username", bsus.Username);
            cmd.Parameters.AddWithValue("@userpassword", bsus.UserPassword);
            cmd.Parameters.AddWithValue("@name", bsus.BusinessName);
            cmd.Parameters.AddWithValue("@phonenum", bsus.PhoneNum);
            cmd.Parameters.AddWithValue("@addressline1", bsus.AddressLine1);
            cmd.Parameters.AddWithValue("@addressline2", bsus.AddressLine2);
            cmd.Parameters.AddWithValue("@city", bsus.City);
            cmd.Parameters.AddWithValue("@county", bsus.County);
            cmd.Parameters.AddWithValue("@eircode", bsus.EirCode);
            cmd.Parameters.AddWithValue("@country", bsus.Country);


            conn.Open();
            object o = cmd.ExecuteScalar();
            conn.Close();

            //--------------------------------------
            //OLD INSERY OF NEW BUSINESS BEFORE CREATED STORED PROC - NOW USE STORED PROC ABOVE
            //--------------------------------------
            ////Create Command For Insert Into Login Details Table
            //string query = "INSERT INTO [dbo].[LoginDetail]([Email],[Username],[UserPassword]) VALUES(@email,@username,@userpassword)";
            //SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@email", bsus.Email);

            //cmd.Parameters.AddWithValue("@username", bsus.Username);
            //cmd.Parameters.AddWithValue("@userpassword", bsus.UserPassword);

            ////Query the DB
            //cmd.ExecuteNonQuery();

            ////Create Command For Insert Into Business Table
            ////IsVerified is always 0 (false) when creating account
            //query = "INSERT INTO [dbo].[Business]([BusinessName],[PhoneNum],[IsVerified],[Email]) VALUES(@name,@phonenum,@isverified,@email) select scope_identity()"; 
            //cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@name", bsus.BusinessName);

            //cmd.Parameters.AddWithValue("@phonenum", bsus.PhoneNum);
            //cmd.Parameters.AddWithValue("@isverified", 0); //IsVerified is always 0 (false) when creating account
            //cmd.Parameters.AddWithValue("@email", bsus.Email);

            ////Query the DB and store the primary key id of the new entry for use as foreign key in address insert below
            ////Reference https://www.mikesdotnetting.com/Article/54/Getting-the-identity-of-the-most-recently-added-record
            //int newpkid = Convert.ToInt32(cmd.ExecuteScalar());


            ////Create Command For Insert Into Address Table
            //query = "INSERT INTO [dbo].[Address]([AddressLine1],[AddressLine2],[City],[County],[EirCode],[Country],[BusinessRegID]) VALUES(@addressline1,@addressline2,@city,@county,@eircode,@country,@businessregid)";
            //cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@addressline1", bsus.AddressLine1);

            //cmd.Parameters.AddWithValue("@addressline2", bsus.AddressLine2);
            //cmd.Parameters.AddWithValue("@city", bsus.City);
            //cmd.Parameters.AddWithValue("@county", bsus.County);
            //cmd.Parameters.AddWithValue("@eircode", bsus.EirCode);
            //cmd.Parameters.AddWithValue("@country", bsus.Country);
            //cmd.Parameters.AddWithValue("@businessregid", newpkid);

            ////Query the DB
            //cmd.ExecuteNonQuery();

            //Close connection
            //conn.Close();

            return View(bsus);
        }




        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


    }
}
