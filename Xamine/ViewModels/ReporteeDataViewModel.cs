using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Author : Navjit Kaur

namespace Xamine.ViewModels
{
    public class ReporteeDataViewModel
    {
        public string EmpId { get; set; }
        public string HoursAssigned { get; set; }
        public string TaskAssigned { get; set; }
        public string TaskPriority { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
        public string Status { get; set; }
    }
}