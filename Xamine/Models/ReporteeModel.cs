using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//Auhtor : Navjit Kaur

namespace Xamine.Models
{
    [Table("reportee")]
    public class ReporteeModel : UserModel
    {
        [ForeignKey("ProjectModel")]
        public string ProjectRefId { get; set; }
        public ProjectModel ProjectModel { get; set; }

        public int HoursAssigned { get; set; }

        public int HoursWorked { get; set; }

        public string TaskAssigned { get; set; }

        public string TaskPriority { get; set; }

    }
}