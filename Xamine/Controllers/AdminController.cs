using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {

        private ApplicationDbContext _context;
        private AdminModel currentAdmin;

        public AdminController()
        {
            _context = new ApplicationDbContext();
            string id= CookieStore.GetCookie("EmpId");
            currentAdmin = _context.Admins.SingleOrDefault(a => a.EmpId.Equals(id));
        }

        //Admin Dashboard
        [AllowAnonymous]
        public ActionResult AdminDashboard()
        {
            return View(_context);
        }

        //Add Manager action for GET request
        public ActionResult AddManager()
        {

            return View();
        }

        //Add Manager action for POST request
        [HttpPost]
        public ActionResult AddManager(ManagerModel manager)
        {
            //if all validations are valid
            if (ModelState.IsValid)
            {
                if (_context.Managers.ToList().Count != 0)
                {
                    ManagerModel lastmanager = _context.Managers.ToList().Last();
                    string[] id = lastmanager.EmpId.Split('-');
                    int empid = Convert.ToInt16(id[1]);
                    empid += 1;
                    manager.EmpId = "M-" + empid;
                }
                else
                {
                    manager.EmpId = "M-101";
                }
                //add manager into list.
                _context.Managers.Add(manager);
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }

            //passing new ManagerModel to view
            var managerModel = new ManagerModel();
            return View(managerModel);
        }

        //Add Reportee action for GET request
        public ActionResult AddReportee()
        {

            return View();
        }

        //Add Reportee action for POST request
        [HttpPost]
        public ActionResult AddReportee(ReporteeModel reportee)
        {
            //if all validations are valid
            if (ModelState.IsValid)
            {
                if (_context.Reportees.ToList().Count != 0)
                {
                    ReporteeModel lastreportee = _context.Reportees.ToList().Last();
                    string[] id = lastreportee.EmpId.Split('-');
                    int empid = Convert.ToInt16(id[1]);
                    empid += 1;
                    reportee.EmpId = "R-" + empid;
                }
                else
                {
                    reportee.EmpId = "R-101";
                }
                //add reportee into list
                _context.Reportees.Add(reportee);
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();
            }

            //passing new ReporteeModel to view.
            var reporteeModel = new ReporteeModel();
            return View(reporteeModel);

        }

        //Partial View responding to GET requests for Update Manager
        [HttpGet]
        public ActionResult UpdateManagerPartialView(string id)
        {
            ManagerModel managerModel = _context.Managers.SingleOrDefault(m => m.EmpId.Equals(id));
            return PartialView(managerModel);

        }

        //Partial View responding to POST requests for Update Manager
        [HttpPost]
        public ActionResult UpdateManagerPartialView(ManagerModel manager)
        {
            //if model state is valid
            if (ModelState.IsValid)
            {
                if (_context.Managers.ToList().Count != 0)
                {
                    //getting the previous details of manager
                    ManagerModel prevmanager = _context.Managers.ToList().SingleOrDefault(m => m.EmpId == manager.EmpId);
                    if (prevmanager != null)
                    {
                        //updating with new values
                        prevmanager.Fname = manager.Fname;
                        prevmanager.Lname = manager.Lname;
                        prevmanager.Gender = manager.Gender;
                        prevmanager.DOB = manager.DOB;
                        prevmanager.Contact = manager.Contact;
                        prevmanager.Email = manager.Email;

                    }
                }
                //saving changes
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateManager");
        }

        //Update Reportee action for GET request
        public ActionResult UpdateManager()
        {

            return View(_context);
        }

        //Delete Manager action for POST request
        [HttpPost]
        public ActionResult DeleteManager(string id)
        {
            // if state is valid
            if (ModelState.IsValid)
            {
                if (_context.Managers.ToList().Count != 0)
                {
                    //finding the manager in database
                    ManagerModel manager = _context.Managers.ToList().SingleOrDefault(m => m.EmpId.Equals(id));
                    if (manager != null)
                    {
                        //deleting manager from database
                        _context.Managers.Remove(manager);
                    }
                }
                //saving changes
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateManager");
        }

        //Partial View responding to GET requests for Update Manager
        [HttpGet]
        public ActionResult UpdateReporteePartialView(string id)
        {
            ReporteeModel reporteeModel = _context.Reportees.SingleOrDefault(m => m.EmpId.Equals(id));
            return PartialView(reporteeModel);

        }

        //Partial View responding to POST requests for Update Manager
        [HttpPost]
        public ActionResult UpdateReporteePartialView(ReporteeModel reportee)
        {
            //if model state is valid
            if (ModelState.IsValid)
            {
                if (_context.Reportees.ToList().Count != 0)
                {
                    //find the reportee from database
                    ReporteeModel prevReportee = _context.Reportees.ToList().SingleOrDefault(m => m.EmpId == reportee.EmpId);
                    if (prevReportee != null)
                    {
                        //update the values with new ones
                        prevReportee.Fname = reportee.Fname;
                        prevReportee.Lname = reportee.Lname;
                        prevReportee.Gender = reportee.Gender;
                        prevReportee.DOB = reportee.DOB;
                        prevReportee.Contact = reportee.Contact;
                        prevReportee.Email = reportee.Email;

                    }
                }
                //save changes
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateReportee");
        }


        //Update Reportee action for GET request
        public ActionResult UpdateReportee()
        {

            return View(_context);
        }

        [HttpPost]
        public ActionResult DeleteReportee(string id)
        {
            //if model state is valid
            if (ModelState.IsValid)
            {
                if (_context.Reportees.ToList().Count != 0)
                {
                    //finding the reportee from database
                    ReporteeModel reportee = _context.Reportees.ToList().SingleOrDefault(m => m.EmpId.Equals(id));
                    if (reportee != null)
                    {
                        //deleting the reportee from database
                        _context.Reportees.Remove(reportee);
                    }
                }
                //saving changes
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateReportee");
        }

        //Logout action
        [AllowAnonymous]
        public ActionResult Logout()
        {
            CookieStore.RemoveCookie("EmpId");
            return RedirectToAction("Login", "Login");
        }

    }
}