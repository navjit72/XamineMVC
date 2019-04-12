using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xamine.Models;

namespace Xamine.ViewModels
{
    public class ReporteeProjectViewModel
    {
        public List<ReporteeModel> AllReportees { get; set; }
        
        public ProjectModel ProjectModel { get; set; }

    }
}