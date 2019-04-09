using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//Auhtor : Navjit Kaur

namespace Xamine.Models
{
    [Table("project")]
    public class ProjectModel
    {
        [Key]
        [StringLength(7)]
        public string ProjectId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public int Progress { get; set; }
    }
}