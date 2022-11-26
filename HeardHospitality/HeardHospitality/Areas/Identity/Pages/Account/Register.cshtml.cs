// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using HeardHospitality.Models;
using HeardHospitality.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Data.SqlClient;
using System.Security.Claims;



using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace HeardHospitality.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<LoginDetail> _signInManager;
        private readonly UserManager<LoginDetail> _userManager;
        private readonly IUserStore<LoginDetail> _userStore;
        private readonly IUserEmailStore<LoginDetail> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public RegisterModel(
            UserManager<LoginDetail> userManager,
            IUserStore<LoginDetail> userStore,
            SignInManager<LoginDetail> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IConfiguration config)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _configuration = config;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        ///     
        /// 
        [BindProperty]
        public bool isBusinessAccount { get; set; }

        [BindProperty]
        public bool isEmployeeAccount { get; set; }

        [BindProperty]
        public string BusinessName { get; set; }

        [BindProperty]
        public string PhoneNum { get; set; }


        /// </summary>
        public class InputModel
        {
            [BindProperty]
            [Display(Name = "Business Account")]
            public bool isBusinessAccount { get; set; }

            [BindProperty]
            [Display(Name = "Employee Account")]
            public bool isEmployeeAccount { get; set; }

            [BindProperty]
            [Display(Name = "Business Name")]
            public string BusinessName { get; set; }

            [BindProperty]
            [Display(Name = "Phone Number")]
            public string PhoneNum { get; set; }


            [BindProperty]
            [Display(Name = "Address Line 1")]
            public string? AddressLine1 { get; set; }


            [BindProperty]
            [Display(Name = "Address Line 2")]
            public string? AddressLine2 { get; set; }


            [BindProperty]
            [Display(Name = "City")]
            public string City { get; set; }


            [BindProperty]
            [Display(Name = "County")]
            public string County { get; set; }


            [BindProperty]
            [Display(Name = "EirCode")]
            public string? EirCode { get; set; }


            [BindProperty]
            [Display(Name = "Country")]
            public string Country { get; set; }



            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null, string id = "EmployeeAccount")
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            RegisterModel rm = new RegisterModel(_userManager, _userStore, _signInManager, _logger, _emailSender, _configuration);

            if (id == "BusinessAccount")
            {
                isEmployeeAccount = false;
                isBusinessAccount = true;
            }
            else
            {
                isBusinessAccount = false;
                isEmployeeAccount = true;
            }

        }



        //public async Task<IActionResult> OnContinueAsync()
        //{

        //}

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                bool isEmp = true;
                bool isBus = false;

                if (Input.isBusinessAccount == true)
                {
                    isEmp = false;
                    isBus = true;
                }
                var user = new LoginDetail { DateCreated = DateTime.Now, isEmployeeAccount = isEmp, isBusinessAccount = isBus };

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);



                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);



                    if (Input.isEmployeeAccount == true)
                    {
                        //create record in employee table linking to new user
                        var employee = new Employee { FirstName = "", LastName = "", City = "", County = "", Gender = "", Phone = "", EmpBio = "", DesiredJob = "", IsSearching = false, IsVisible = false, LoginDetails = user };

                        string connStr = _configuration.GetConnectionString("DefaultConnection");
                        SqlConnection conn = new SqlConnection(connStr);

                        string query = "INSERT INTO dbo.Employee (FirstName, LastName, City, County, Gender, Phone, EmpBio, DesiredJob, IsSearching, IsVisible, LoginDetailsId) VALUES (@FirstName, @LastName, @City, @County, @Gender, @Phone, @EmpBio, @DesiredJob, @IsSearching, @IsVisible, @LoginDetailsId)";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@FirstName", "");
                        cmd.Parameters.AddWithValue("@LastName", "");
                        cmd.Parameters.AddWithValue("@City", "");
                        cmd.Parameters.AddWithValue("@County", "");
                        cmd.Parameters.AddWithValue("@Gender", "");
                        cmd.Parameters.AddWithValue("@Phone", "");
                        cmd.Parameters.AddWithValue("@EmpBio", "");
                        cmd.Parameters.AddWithValue("@DesiredJob", "");
                        cmd.Parameters.AddWithValue("@IsSearching", 0);
                        cmd.Parameters.AddWithValue("@IsVisible", 0);
                        cmd.Parameters.AddWithValue("@LoginDetailsId", user.Id);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();



                    }
                    else if (Input.isBusinessAccount == true)
                    {
                        //create record in business table linking to new user
                        var business = new Business { BusinessName = "", PhoneNum = "", IsVerified = false, LoginDetails = user };

                        string connStr = _configuration.GetConnectionString("DefaultConnection");
                        SqlConnection conn = new SqlConnection(connStr);

                        string query = "INSERT INTO dbo.Business (BusinessName, PhoneNum, IsVerified, LoginDetailsId) VALUES (@BusinessName, @PhoneNum, @IsVerified, @LoginDetailsId) " +
                                        "SELECT SCOPE_IDENTITY()";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@BusinessName", Input.BusinessName);
                        cmd.Parameters.AddWithValue("@PhoneNum", Input.PhoneNum);
                        cmd.Parameters.AddWithValue("@IsVerified", "");
                        cmd.Parameters.AddWithValue("@LoginDetailsId", user.Id);

                        conn.Open();
                        int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                        //int lastId = (int)cmd.ExecuteScalar();
                        conn.Close();



                        //get current business user's ID to allow display data
                       
                        connStr = _configuration.GetConnectionString("DefaultConnection");
                        conn = new SqlConnection(connStr);


                        //query = "SELECT * FROM Business WHERE Business.LoginDetailsId = @LoginDetailsId";

                        //cmd = new SqlCommand(query, conn);

                        //cmd.Parameters.AddWithValue("@LoginDetailsId", lastId);



                        //create record in address table linking to new user
                        var address = new Address { AddressLine1 = "", AddressLine2 = "", City = "", County = "", EirCode = "", Country = "", BusinessID = lastId};

                        query = "insert into dbo.Address (AddressLine1, AddressLine2, City, County, EirCode, Country, BusinessId) " +
                                "values (@addressline1, @addressline2, @city, @county, @eircode, @country, @businessid)";

                        cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@addressline1", "");
                        cmd.Parameters.AddWithValue("@addressline2", "");
                        cmd.Parameters.AddWithValue("@city", Input.City);
                        cmd.Parameters.AddWithValue("@county", Input.County);
                        cmd.Parameters.AddWithValue("@eircode", "");
                        cmd.Parameters.AddWithValue("@country", "");
                        cmd.Parameters.AddWithValue("@businessid", lastId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();


                    }


                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }



                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private LoginDetail CreateUser()
        {
            try
            {
                return Activator.CreateInstance<LoginDetail>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(LoginDetail)}'. " +
                    $"Ensure that '{nameof(LoginDetail)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<LoginDetail> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<LoginDetail>)_userStore;
        }
    }
}
