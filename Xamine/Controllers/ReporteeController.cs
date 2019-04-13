using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;
using Xamine.ViewModels;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    public class ReporteeController : Controller
    {
        private ReporteeModel currentReportee = new ReporteeModel();
        private ApplicationDbContext _context;

        public ReporteeController()
        {
            _context = new ApplicationDbContext();
            string id = CookieStore.GetCookie("EmpId");
            currentReportee = _context.Reportees.SingleOrDefault(r => r.EmpId.Equals(id));
        }

        [CustomAuthorize]
        //ReporteeDashboard
        public ActionResult Dashboard()
        {
            //collecting the required values into the view model and passing it to view
            ReporteeViewModel reporteeView = new ReporteeViewModel();
            if (currentReportee == null)
                return View(reporteeView);
            reporteeView.EmpId = currentReportee.EmpId;
            reporteeView.HoursAssigned = currentReportee.HoursAssigned;
            reporteeView.HoursWorked = currentReportee.HoursWorked;
            reporteeView.TaskAssigned = currentReportee.TaskAssigned;
            reporteeView.TaskPriority = currentReportee.TaskPriority;
            reporteeView.ProjectModel = new ProjectModel();
            if (currentReportee.ProjectRefId != null)
            {
                //getting the project name
                ProjectModel project = _context.Projects.SingleOrDefault(p => p.ProjectId.Equals(currentReportee.ProjectRefId));
                reporteeView.ProjectModel.Name = project.Name;
            }
            else
            {
                //if no project has yet been assigned to reportee
                reporteeView.ProjectModel.Name = "No project yet";
                reporteeView.TaskAssigned = "N/A";
                reporteeView.TaskPriority = "N/A";
            }
            return View(reporteeView);
        }

        //Update daily progress of task
        [HttpPost]
        [CustomAuthorize]
        public ActionResult UpdateDailyProgress(ReporteeViewModel reporteeModel)
        {
            if (currentReportee.ProjectRefId != null)
            {
                //previous worked hours
                int hoursWorked = currentReportee.HoursWorked;
                //updating with new ones
                currentReportee.HoursWorked = hoursWorked + reporteeModel.HoursWorked;
                //saving changes
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
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