using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xamine.Models;

//Author : Navjit Kaur

namespace Xamine.ViewModels
{
    public class Login_ChangePasswordViewModel
    {
        public LoginModel loginModel { get; set; }
        public ChangePasswordModel changePasswordModel { get; set; }
    }
}