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

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


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
        /// </summary>
        public class InputModel
        {

            [Display(Name = "Business Account")]
            public bool isBusinessAccount { get; set; }

            [Display(Name = "Employee Account")]
            public bool isEmployeeAccount { get; set; }


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


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //var user = CreateUser();
                var user = new LoginDetail { DateCreated = DateTime.Now, isEmployeeAccount = Input.isEmployeeAccount, isBusinessAccount = Input.isBusinessAccount };

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);




                if (Input.isEmployeeAccount == true)
                {
                    //create record in employee table linking to new user
                    var employee = new Employee { FirstName = "", LastName = "", City = "", County = "", Gender = "", Phone = "", EmpBio = "", DesiredJob = "", IsSearching = false, IsVisible = false, LoginDetails = user };


                    string connStr = _configuration.GetConnectionString("DefaultConnection");
                    SqlConnection conn = new SqlConnection(connStr);

                    string query = "INSERT INTO dbo.Employee (FirstName, LastName, City, County, Gender, Phone, EmpBio, DesiredJob, IsSearching, IsVisible, LoginDetailsId) VALUES (@FirstName, @LastName, @City, @County, @Gender, @Phone, @EmpBio, @DesiredJob, @IsSearching, @IsVisible, @LoginDetailsId)";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    //cmd.Connection = conn;
                    //cmd.CommandType = System.Data.CommandType.Insert;
                    //cmd.CommandText = "uspNewEmployee";

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
                    //object o = cmd.ExecuteScalar();
                    conn.Close();


                    //if (await TryUpdateModelAsync<Employee>(employee))
                    // {

                    //_context.Employee.Add(employee);
                    //await _context.SaveChangesAsync();
                    //   }

                }
                else if (Input.isBusinessAccount == true)
                {

                    //create record in business table linking to new user
                }


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
