using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class OwnerModel
    {
        public int owner_id { get; set; }
        
        [Display(Name = "Parent practice")]
        public int parent_id { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "Name length cannot exceed 250 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }

        public List<owner> lstPossibleParentPractices { get; set; }

        [Display(Name = "Approver(s)")]
        public List<string> owner_approvers { get; set; }
        
        [Display(Name = "Contacts")]
        public List<string> owner_contacts { get; set; }

        [Display(Name = "Department(s)")]
        public List<string> owner_departments { get; set; }
    }
}