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

        public List<ProjectModel> Project { get; set; }

        public int HoursAssigned { get; set; }

        public int HoursWorked { get; set; }

        public string TaskAssigned { get; set; }

        public string TaskPriority { get; set; }

    }
}