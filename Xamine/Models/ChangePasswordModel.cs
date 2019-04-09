using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Confirm New Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New Password does not match with Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "")]
        public string Error { get; set; }
    }
}