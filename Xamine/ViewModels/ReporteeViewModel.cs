using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xamine.Models;

//Author : Navjit Kaur

namespace Xamine.ViewModels
{
    public class ReporteeViewModel
    {
        public string EmpId { get; set; }

        public ProjectModel ProjectModel { get; set; }

        public int HoursAssigned { get; set; }

        public int HoursWorked { get; set; }

        public string TaskAssigned { get; set; }

        public string TaskPriority { get; set; }
    }
}