using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class TeamRoleModel
    {
        public int team_role_id { get; set; }

        public int display_order { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name length cannot exceed 50 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }

        [Display(Name = "Legacy")]
        public bool legacy { get; set; }

        [Display(Name = "Multiple names")]
        public bool can_multiple { get; set; }

        [Display(Name = "Mandatory")]
        public bool mandatory { get; set; }
    }
}