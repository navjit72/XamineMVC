using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamine.Models;
using Xamine.ViewModels;

//Author : Navjit Kaur

namespace Xamine.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationDbContext _context;
        List<AdminModel> adminList;
        List<ManagerModel> managerList;
        List<ReporteeModel> reporteeList;

        public LoginController()
        {
            _context = new ApplicationDbContext();
            adminList = _context.Admins.ToList();
            managerList = _context.Managers.ToList();
            reporteeList = _context.Reportees.ToList();
        }


        //Login action for GET request
        public ActionResult Login()
        {
            if (CookieStore.GetCookie("EmpId")!=string.Empty)
            {
                string id = CookieStore.GetCookie("EmpId");
                if (id.StartsWith("A"))
                    return RedirectToAction("AdminDashboard", "Admin");
                else if (id.StartsWith("M"))
                    return RedirectToAction("Dashboard", "Manager");
                else if (id.StartsWith("R"))
                    return RedirectToAction("Dashboard", "Reportee");
            }

            Login_ChangePasswordViewModel loginViewModel = new Login_ChangePasswordViewModel
            {
                loginModel = new LoginModel(),
                changePasswordModel = new ChangePasswordModel()
            };
            return View(loginViewModel);

        }

        //Login action for POST request
        [HttpPost]

        public ActionResult Login(Login_ChangePasswordViewModel viewModel)
        {
            if (Request.Form["LoginButton"] != null)
            {

                //if validations are valid
                if (ModelState.IsValid)
                {
                    //checking the type of user trying to login
                    if (viewModel.loginModel.EmpId.ElementAt(0) == 'A')
                    {
                        var admin = adminList.SingleOrDefault(m => m.EmpId == viewModel.loginModel.EmpId);
                        if (admin != null)
                        {
                            if (admin.Password.Equals(viewModel.loginModel.Password))
                            {
                                CookieStore.SetCookie("EmpId", admin.EmpId);
                                return RedirectToAction("AdminDashboard", "Admin");
                            }
                            else
                            {

                                viewModel.loginModel.Error = "Password Incorrect";

                            }
                        }
                        else
                        {
                            viewModel.loginModel.Error = "Admin with given Id does not exist";

                        }

                    }
                    else if (viewModel.loginModel.EmpId.ElementAt(0) == 'M')
                    {
                        var manager = managerList.SingleOrDefault(m => m.EmpId == viewModel.loginModel.EmpId);
                        if (manager != null)
                        {
                            if (manager.Password.Equals(viewModel.loginModel.Password))
                            {
                                CookieStore.SetCookie("EmpId", manager.EmpId);
                                return RedirectToAction("Dashboard", "Manager");
                            }
                            else
                            {
                                viewModel.loginModel.Error = "Password Incorrect";

                            }
                        }
                        else
                        {
                            viewModel.loginModel.Error = "Manager with given Id does not exist";

                        }
                    }
                    else if (viewModel.loginModel.EmpId.ElementAt(0) == 'R')
                    {
                        var reportee = reporteeList.SingleOrDefault(m => m.EmpId == viewModel.loginModel.EmpId);
                        if (reportee != null)
                        {
                            if (reportee.Password.Equals(viewModel.loginModel.Password))
                            {
                                CookieStore.SetCookie("EmpId", reportee.EmpId);
                                return RedirectToAction("Dashboard", "Reportee");
                            }
                            else
                            {
                                viewModel.loginModel.Error = "Password Incorrect";

                            }
                        }
                        else
                        {
                            viewModel.loginModel.Error = "Reportee with given Id does not exist";

                        }
                    }
                    else
                    {
                        viewModel.loginModel.Error = "Incorrect Id";

                    }

                }
                viewModel.changePasswordModel = new ChangePasswordModel();
            }
            else if (Request.Form["SaveButton"] != null)
            {
                if (ModelState.IsValid)
                {


                    //checking the type of user trying to login
                    if (viewModel.changePasswordModel.EmpId.ElementAt(0) == 'A')
                    {
                        var admin = adminList.SingleOrDefault(m => m.EmpId == viewModel.changePasswordModel.EmpId);
                        if (admin != null)
                        {
                            if (admin.Password.Equals(viewModel.changePasswordModel.Password))
                            {
                                admin.Password = viewModel.changePasswordModel.NewPassword;

                                try
                                {
                                    _context.SaveChanges();
                                }
                                catch (DbEntityValidationException dbEx)
                                {
                                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                                    {
                                        foreach (var validationError in validationErrors.ValidationErrors)
                                        {
                                            Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                        }
                                    }
                                }
                                viewModel = new Login_ChangePasswordViewModel()
                                {
                                    changePasswordModel = new ChangePasswordModel()
                                };

                            }
                            else
                            {
                                viewModel.changePasswordModel.Error = "Password Incorrect";
                            }

                        }
                        else
                        {
                            viewModel.changePasswordModel.Error = "Admin with given Id does not exist";
                        }
                    }
                    else if (viewModel.changePasswordModel.EmpId.ElementAt(0) == 'M')
                    {
                        var manager = managerList.SingleOrDefault(m => m.EmpId == viewModel.changePasswordModel.EmpId);
                        if (manager != null)
                        {
                            if (manager.Password.Equals(viewModel.changePasswordModel.Password))
                            {
                                manager.Password = viewModel.changePasswordModel.NewPassword;
                                try
                                {
                                    _context.SaveChanges();
                                }
                                catch (DbEntityValidationException dbEx)
                                {
                                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                                    {
                                        foreach (var validationError in validationErrors.ValidationErrors)
                                        {
                                            Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                        }
                                    }
                                }
                                viewModel = new Login_ChangePasswordViewModel()
                                {
                                    changePasswordModel = new ChangePasswordModel()
                                };

                            }
                            else
                            {
                                viewModel.changePasswordModel.Error = "Password Incorrect";
                            }
                        }
                        else
                        {
                            viewModel.changePasswordModel.Error = "Manager with given Id does not exist";
                        }
                    }
                    else if (viewModel.changePasswordModel.EmpId.ElementAt(0) == 'R')
                    {
                        var reportee = reporteeList.SingleOrDefault(m => m.EmpId == viewModel.changePasswordModel.EmpId);
                        if (reportee != null)
                        {
                            if (reportee.Password.Equals(viewModel.changePasswordModel.Password))
                            {
                                reportee.Password = viewModel.changePasswordModel.NewPassword;
                                try
                                {
                                    _context.SaveChanges();
                                }
                                catch (DbEntityValidationException dbEx)
                                {
                                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                                    {
                                        foreach (var validationError in validationErrors.ValidationErrors)
                                        {
                                            Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                        }
                                    }
                                }
                                viewModel = new Login_ChangePasswordViewModel()
                                {
                                    changePasswordModel = new ChangePasswordModel()
                                };
                            }
                            else
                            {
                                viewModel.changePasswordModel.Error = "Password Incorrect";
                            }
                        }
                        else
                        {
                            viewModel.changePasswordModel.Error = "Reportee with given Id does not exist";
                        }
                    }
                    else
                    {
                        viewModel.changePasswordModel.Error = "Incorrect Id";
                    }
                }
                viewModel.loginModel = new LoginModel();

            }

            return View(viewModel);

        }
    }
}