using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//Author : Navjit Kaur

namespace Xamine.Models
{
    [Table("manager")]
    public class ManagerModel : UserModel
    {
        public List<ProjectModel> Project { get; set; }
    }
}