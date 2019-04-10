using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    public class AdminController : Controller
    {

        private ApplicationDbContext _context;
        private static AdminModel currentAdmin;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        //Admin Dashboard
        public ActionResult AdminDashboard()
        {
            if (currentAdmin == null)
            {
                string currentAdminId = TempData["EmpId"].ToString();
                currentAdmin = _context.Admins.SingleOrDefault(a => a.EmpId.Equals(currentAdminId));
            }
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

        [HttpGet]
        public ActionResult UpdateManagerPartialView(string id)
        {
            ManagerModel managerModel = _context.Managers.SingleOrDefault(m => m.EmpId.Equals(id));
            return PartialView(managerModel);

        }

        [HttpPost]
        public ActionResult UpdateManagerPartialView(ManagerModel manager)
        {
            if (ModelState.IsValid)
            {
                if (_context.Managers.ToList().Count != 0)
                {
                    ManagerModel prevmanager = _context.Managers.ToList().SingleOrDefault(m => m.EmpId == manager.EmpId);
                    if (prevmanager != null)
                    {
                        prevmanager.Fname = manager.Fname;
                        prevmanager.Lname = manager.Lname;
                        prevmanager.Gender = manager.Gender;
                        prevmanager.DOB = manager.DOB;
                        prevmanager.Contact = manager.Contact;
                        prevmanager.Email = manager.Email;

                    }
                }
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

        [HttpPost]
        public ActionResult DeleteManager(string id)
        {
            if (ModelState.IsValid)
            {
                if (_context.Managers.ToList().Count != 0)
                {
                    ManagerModel manager = _context.Managers.ToList().SingleOrDefault(m => m.EmpId.Equals(id));
                    if (manager != null)
                    {
                        _context.Managers.Remove(manager);
                    }
                }
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateManager");
        }

        [HttpGet]
        public ActionResult UpdateReporteePartialView(string id)
        {
            ReporteeModel reporteeModel = _context.Reportees.SingleOrDefault(m => m.EmpId.Equals(id));
            return PartialView(reporteeModel);

        }

        [HttpPost]
        public ActionResult UpdateReporteePartialView(ReporteeModel reportee)
        {
            if (ModelState.IsValid)
            {
                if (_context.Reportees.ToList().Count != 0)
                {
                    ReporteeModel prevReportee = _context.Reportees.ToList().SingleOrDefault(m => m.EmpId == reportee.EmpId);
                    if (prevReportee != null)
                    {
                        prevReportee.Fname = reportee.Fname;
                        prevReportee.Lname = reportee.Lname;
                        prevReportee.Gender = reportee.Gender;
                        prevReportee.DOB = reportee.DOB;
                        prevReportee.Contact = reportee.Contact;
                        prevReportee.Email = reportee.Email;

                    }
                }
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
            if (ModelState.IsValid)
            {
                if (_context.Reportees.ToList().Count != 0)
                {
                    ReporteeModel reportee = _context.Reportees.ToList().SingleOrDefault(m => m.EmpId.Equals(id));
                    if (reportee != null)
                    {
                        _context.Reportees.Remove(reportee);
                    }
                }
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateReportee");
        }

        //Logout action
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }

    }
}