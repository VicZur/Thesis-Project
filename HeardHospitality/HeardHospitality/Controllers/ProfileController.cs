using HeardHospitality.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;

namespace HeardHospitality.Controllers
{
    public class ProfileController : Controller
    {
        // GET: UpdateEmployeeProfileController
        public IActionResult UpdateProfile(Employee e)
        {
            return View(e);
        }

        // POST: UpdateEmployeeProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        public IActionResult UpdateWorkHistory(EmployeeExperience ee)
        {
            return View(ee);
        }












        // GET: UpdateEmployeeProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UpdateEmployeeProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpdateEmployeeProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UpdateEmployeeProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }



        // GET: UpdateEmployeeProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UpdateEmployeeProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
