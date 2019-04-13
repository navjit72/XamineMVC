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
    [CustomAuthorize]
    public class ManagerController : Controller
    {

        private ApplicationDbContext _context;
        private ManagerModel currentManager;
        private static List<ReporteeDataViewModel> reporteesAdded = new List<ReporteeDataViewModel>();


        public ManagerController()
        {
            _context = new ApplicationDbContext();
            string id = CookieStore.GetCookie("EmpId");
            currentManager = _context.Managers.Include(p => p.Project).SingleOrDefault(m => m.EmpId.Equals(id));
        }

        //Manager Dashboard
        public ActionResult Dashboard()
        {
            return View(currentManager);
        }

        [CustomAuthorize]
        //Add Project action for GET request
        public ActionResult AddProject()
        {
            List<ReporteeModel> reporteeList = new List<ReporteeModel>();
            foreach (ReporteeModel reportee in _context.Reportees)
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

                //add project into list
                viewModel.ProjectModel.ManagerRefId = currentManager.EmpId;

                List<ReporteeModel> addedReportees = new List<ReporteeModel>();

                foreach (ReporteeDataViewModel entry in reporteesAdded)
                {
                    ReporteeModel repor = _context.Reportees.SingleOrDefault(r => r.EmpId.Equals(entry.EmpId));
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
            List<ProjectModel> projectList = new List<ProjectModel>();
            foreach (ProjectModel proj in _context.Projects)
            {
                if (proj.ManagerRefId.Equals(currentManager.EmpId))
                    projectList.Add(proj);
            }
            return View(projectList);
        }

        [HttpGet]
        public ActionResult UpdateProjectPartialView(string id)
        {
            ProjectModel projectModel = _context.Projects.Include(p => p.Reportees).SingleOrDefault(m => m.ProjectId.Equals(id));

            return PartialView(projectModel);

        }

        [HttpPost]
        public ActionResult UpdateReporteeInProject(List<ReporteeDataViewModel> jsonData)
        {
            reporteesAdded = jsonData;
            ReporteeDataViewModel reporteeData = reporteesAdded.First();
            ProjectModel projectModel = new ProjectModel();
            projectModel.ProjectId = reporteeData.ProjectId;
            projectModel.Name = reporteeData.ProjectName;
            projectModel.Description = reporteeData.Description;
            projectModel.Progress = Convert.ToInt16(reporteeData.Progress);
            projectModel.Status = reporteeData.Status;

            if (ModelState.IsValid)
            {
                if (_context.Projects.ToList().Count != 0)
                {
                    ProjectModel prevProject = _context.Projects.Include(c => c.Reportees).SingleOrDefault(m => m.ProjectId == projectModel.ProjectId);
                    List<ReporteeModel> prevReportees = prevProject.Reportees;
                    if (prevProject != null)
                    {
                        prevProject.Name = projectModel.Name;
                        prevProject.Description = projectModel.Description;
                        prevProject.Progress = projectModel.Progress;
                        prevProject.Status = projectModel.Status;
                        prevProject.ManagerRefId = currentManager.EmpId;

                        List<ReporteeModel> currentReportees = new List<ReporteeModel>();

                        foreach (ReporteeDataViewModel entry in reporteesAdded)
                        {
                            if (entry.EmpId != null)
                            {
                                ReporteeModel repor = prevReportees.SingleOrDefault(r => r.EmpId.Equals(entry.EmpId));
                                repor.HoursAssigned = Convert.ToInt16(entry.HoursAssigned);
                                repor.TaskAssigned = entry.TaskAssigned;
                                repor.TaskPriority = entry.TaskPriority;
                                currentReportees.Add(repor);
                            }
                        }

                        foreach (ReporteeModel reportee in prevReportees)
                        {
                            if (currentReportees.Contains(reportee) == false)
                            {
                                reportee.ProjectRefId = null;
                                reportee.HoursAssigned = 0;
                                reportee.TaskAssigned = null;
                                reportee.TaskPriority = null;
                            }

                        }
                        prevProject.Reportees = currentReportees;
                    }
                }
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

                if (reporteesAdded != null)
                    reporteesAdded.Clear();

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }


        //Delete Project
        [HttpPost]
        public ActionResult DeleteProject(string id)
        {
            if (ModelState.IsValid)
            {
                if (_context.Projects.ToList().Count != 0)
                {
                    ProjectModel project = _context.Projects.Include(c => c.Reportees).SingleOrDefault(m => m.ProjectId.Equals(id));
                    if (project != null)
                    {
                        project.ManagerRefId = null;
                        foreach (ReporteeModel reportee in project.Reportees)
                            reportee.ProjectRefId = null;
                        _context.Projects.Remove(project);
                    }
                }
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();

            }
            return RedirectToAction("UpdateProject");
        }


        //Generate Project Statistics
        [HttpGet]
        public ActionResult ProjectStatistics(string id)
        {
            ProjectModel project = _context.Projects.Include(c => c.Reportees).SingleOrDefault(p => p.ProjectId.Equals(id));
            List<string> labels = new List<string> { "PLAN", "DESIGN", "BUILD", "TEST", "DEPLOY", "MAINTAIN" };
            List<int> chartValues = new List<int>() {0,0,0,0,0,0 };
            if(project.Reportees != null)
            {
                foreach(ReporteeModel reportee in project.Reportees)
                {
                    switch (reportee.TaskAssigned)
                    {
                        case "PLAN":
                            chartValues[0] = reportee.HoursAssigned + chartValues.ElementAt(0);
                            break;
                        case "DESIGN":
                            chartValues[1] = reportee.HoursAssigned + chartValues.ElementAt(1);
                            break;
                        case "BUILD":
                            chartValues[2] = reportee.HoursAssigned + chartValues.ElementAt(2);
                            break;
                        case "TEST":
                            chartValues[3] = reportee.HoursAssigned + chartValues.ElementAt(3);
                            break;
                        case "DEPLOY":
                            chartValues[4] = reportee.HoursAssigned + chartValues.ElementAt(4);
                            break;
                        case "MAINTAIN":
                            chartValues[5] = reportee.HoursAssigned + chartValues.ElementAt(5);
                            break;
                    }
                }
                var chartLabels = labels;
                var cValues = chartValues;
                ViewBag.LABELS = chartLabels;
                ViewBag.CHARTVALUES = cValues;
            }
            return View();
        }

        //Generate Reportee Statistics
        public ActionResult ReporteeStatistics()
        {
            return View();
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