﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    public class ManagerController : Controller
    {

        private ApplicationDbContext _context;

        public ManagerController()
        {
            _context = new ApplicationDbContext();
        }

        //Manager Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        //Add Project action for GET request
        public ActionResult AddProject()
        {
            return View();
        }

        //Add Project action for POST request
        [HttpPost]
        public ActionResult AddProject(ProjectModel project)
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
                    project.ProjectId = "P-" + pid;
                }
                else
                {
                    project.ProjectId = "P-101";
                }
                //add reportee into list
                _context.Projects.Add(project);
                _context.SaveChanges();

                //clearing the state
                ModelState.Clear();
            }

            //passing new ProjectModel to view
            var projectModel = new ProjectModel();
            return View(projectModel);

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
            return RedirectToAction("Login", "Login");
        }
    }
}