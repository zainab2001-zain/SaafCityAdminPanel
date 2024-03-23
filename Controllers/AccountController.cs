using saafcity_fyp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace saafcity_fyp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //login get page
        public ActionResult Login()
        {

            return View();
        }

        //login post
        [HttpPost]
        public ActionResult Login(Admin user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new SaafCity_Database_2Entities())
                    {
                        //check if any username and password match to the one provided

                        bool isValid = context.Admins.Any(x => x.Employee_Id== user.Employee_Id && x.Password == user.Password.ToString());
                        if (isValid)
                        {
                            //setting auth cookie
                            FormsAuthentication.SetAuthCookie(user.Employee_Id.ToString(), false);

                            TempData["message"] = "Login Successful!";

                            return RedirectToAction("Dashboard","Home");
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage");
            }
        }

        public ActionResult ErrorPage()
        {
            ViewBag.eMessage=TempData["message"];
            return View();
        }

        //register
        public ActionResult register()
        {


            return View();
        }
        public ActionResult Logout()
        {
            //logout using form authentication

            FormsAuthentication.SignOut();
            TempData["message"] = "Login Successful!";

            return RedirectToAction("Login");
        }

        //[HttpPost]
        //public ActionResult register(Employee employee)
        //{
        //    try
        //    {
        //        using (var context = new SaafCity_DatabaseEntities1())
        //        {


        //            context.Employees.Add(employee);
        //            context.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["message"] = ex.Message;
        //    }

        //    return View();
        //}

    }
}