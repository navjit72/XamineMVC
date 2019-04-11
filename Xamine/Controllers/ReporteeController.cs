using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    public class ReporteeController : Controller
    {
        //ReporteeDashboard
        public ActionResult Dashboard()
        {
            var model = new ReporteeModel();
            model.Fname = "NAVJIT";
            model.Lname = "KAUR";
            return View(model);
        }

        //Check ratings given by the manager
        public ActionResult Ratings()
        {
            return View();
        }

        //Logout action
        public ActionResult Logout()
        {
            CookieStore.RemoveCookie("EmpId");
            return RedirectToAction("Login", "Login");
        }

    }
}