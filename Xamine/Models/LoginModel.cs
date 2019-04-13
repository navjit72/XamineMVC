using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Xamine.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Provide Employee Id", AllowEmptyStrings = false)]
        [StringLength(7)]
        public string EmpId { get; set; }

        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Error { get; set; }
        public string TimeoutError { get; set; }
    }
}