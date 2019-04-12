using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;
using Xamine.ViewModels;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    public class ManagerController : Controller
    {

        private ApplicationDbContext _context;
        private ManagerModel currentManager;
        private static List<ReporteeDataViewModel> reporteesAdded = new List<ReporteeDataViewModel>();
        //private List<ProjectModel> projectList;

        public ManagerController()
        {
            _context = new ApplicationDbContext();
            string id = CookieStore.GetCookie("EmpId");
            currentManager = _context.Managers.Include(p => p.Project).SingleOrDefault(m => m.EmpId.Equals(id));
            
            //projectList = new List<ProjectModel>();
        }

        //Manager Dashboard
        public ActionResult Dashboard()
        {
                
                //projectList.Add(_context.Projects.SingleOrDefault(p => p.ManagerRefId.Equals(currentUserId)));                
            
            //ViewBag.ProjectList = projectList;
            return View(currentManager);
        }

        //Add Project action for GET request
        public ActionResult AddProject()
        {
            List<ReporteeModel> reporteeList = new List<ReporteeModel>();
            foreach(ReporteeModel reportee in _context.Reportees)
            {
                if (reportee.ProjectRefId == null)
                    
                     reporteeList.Add(reportee);
            }
            ReporteeProjectViewModel viewModel = new ReporteeProjectViewModel()
            {
                ProjectModel = new ProjectModel(),
                AllReportees = reporteeList
            };
            return View(viewModel);
        }

        //Add Project action for POST request
        [HttpPost]
        public ActionResult AddProject(ReporteeProjectViewModel viewModel)
        {
            //if validations are valid
            if (ModelState.IsValid)
            {
                if (_context.Projects.ToList().Count != 0)
                {
                    ProjectModel lastproject = _context.Projects.ToList().Last();
                    string[] id = lastproject.ProjectId.Split('-');
                    int pid = Convert.ToInt16(id[1]);
                    pid += 1;
                    viewModel.ProjectModel.ProjectId = "P-" + pid;
                }
                else
                {
                    viewModel.ProjectModel.ProjectId = "P-101";
                }
                currentManager.Project = new List<ProjectModel>();
                //add project into list
                viewModel.ProjectModel.ManagerRefId = currentManager.EmpId;

                List<ReporteeModel> addedReportees = new List<ReporteeModel>();

                foreach(ReporteeDataViewModel entry in reporteesAdded)
                {
                    ReporteeModel repor = _context.Reportees.SingleOrDefault(r=>r.EmpId.Equals(entry.EmpId));
                    repor.ProjectRefId = viewModel.ProjectModel.ProjectId;
                    
                    repor.HoursAssigned = Convert.ToInt16(entry.HoursAssigned);
                    repor.TaskAssigned = entry.TaskAssigned;
                    repor.TaskPriority = entry.TaskPriority;
                    addedReportees.Add(repor);
                }


                viewModel.ProjectModel.Reportees = addedReportees;


                _context.Projects.Add(viewModel.ProjectModel);
                currentManager.Project.Add(viewModel.ProjectModel);



                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();
            }

            reporteesAdded.Clear();

            return RedirectToAction("AddProject", "Manager");

        }


        //Add reportee to project
        [HttpPost]
        public ActionResult AddReporteeToProject(ReporteeDataViewModel jsonModel)
        {
            reporteesAdded.Add(jsonModel);

            return RedirectToAction("AddProject");
        }

        //Update Project
        public ActionResult UpdateProject()
        {
            return View();
        }

        //Delete Project
        public ActionResult DeleteProject()
        {
            return View();
        }

        //Assign Task to reportee
        public ActionResult AssignTask()
        {
            return View();
        }

        //Provide Ratings to reportee
        public ActionResult RateReportee()
        {
            return View();
        }

        //Generate Project Statistics
        public ActionResult ProjectStatistics()
        {
            return View();
        }

        //Generate Reportee Statistics
        public ActionResult ReporteeStatistics()
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