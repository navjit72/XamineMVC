using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Author : Navjit Kaur

namespace Xamine.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Please Provide Employee Id", AllowEmptyStrings = false)]
        [StringLength(7)]
        public string EmpId { get; set; }

        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Provide New Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 4)]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Confirm New Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New Password does not match with Confirm Password")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 4)]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "")]
        public string Error { get; set; }
    }
}