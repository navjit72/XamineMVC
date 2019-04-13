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

        //ReporteeDashboard
        public ActionResult Dashboard()
        {
            ReporteeViewModel reporteeView = new ReporteeViewModel();
            reporteeView.EmpId = currentReportee.EmpId;
            reporteeView.HoursAssigned = currentReportee.HoursAssigned;
            reporteeView.HoursWorked = currentReportee.HoursWorked;
            reporteeView.TaskAssigned = currentReportee.TaskAssigned;
            reporteeView.TaskPriority = currentReportee.TaskPriority;
            reporteeView.ProjectModel = new ProjectModel();
            if (currentReportee.ProjectRefId != null)
            {
                ProjectModel project = _context.Projects.SingleOrDefault(p => p.ProjectId.Equals(currentReportee.ProjectRefId));
                reporteeView.ProjectModel.Name = project.Name;
            }
            else
            {
                reporteeView.ProjectModel.Name = "No project yet";
                reporteeView.TaskAssigned = "N/A";
                reporteeView.TaskPriority = "N/A";
            }
            return View(reporteeView);
        }

        [HttpPost]
        public ActionResult UpdateDailyProgress(ReporteeViewModel reporteeModel)
        {
            if (currentReportee.ProjectRefId != null)
            {
                int hoursWorked = currentReportee.HoursWorked;
                currentReportee.HoursWorked = hoursWorked + reporteeModel.HoursWorked;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
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