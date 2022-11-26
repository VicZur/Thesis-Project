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



namespace HeardHospitality.Areas.Identity.Pages.Account
{
    public class EmpOrBusinessModel : PageModel
    {

        public class InputModel
        {
            [BindProperty]
            [Display(Name = "Business Account")]
            public bool isBusinessAccount { get; set; }

            [BindProperty]
            [Display(Name = "Employee Account")]
            public bool isEmployeeAccount { get; set; }


        }
        public string ReturnUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");


            if (Input.isEmployeeAccount == true)
            {
                return RedirectToPage("Register", new { id = "EmployeeAccount" });
            }
            else
            {
                return RedirectToPage("Register", new { id = "BusinessAccount" });
            }
        }

    }

}
