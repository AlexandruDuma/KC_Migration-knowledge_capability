using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class DepartmentModel
    {
        public int department_id { get; set; }

        [Display(Name = "Code")] 
        public int code { get; set; }
        
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Allow Initiative Creation")]
        public bool allow_initiative_creation { get; set; }
    }
}