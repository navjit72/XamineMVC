using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xamine.Models;

namespace Xamine.ViewModels
{
    public class ProjectReporteeViewModel
    {
        public List<ReporteeModel> ReporteesInProject { get; set; }
        public ProjectModel ProjectModel { get; set; }
    }
}