using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xamine.ViewModels
{
    public class ReporteeDataViewModel
    {
        public string EmpId { get; set; }
        public string HoursAssigned { get; set; }
        public string TaskAssigned { get; set; }
        public string TaskPriority { get; set; }
    }
}