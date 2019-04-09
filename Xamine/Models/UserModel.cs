using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xamine.Models
{
    public class UserModel
    {
        [Key]
        [StringLength(7)]
        public string EmpId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(255)]
        public string Lname { get; set; }

        [Required]
        public long Contact { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [Required]
        [Min18Year]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime? DOB { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}