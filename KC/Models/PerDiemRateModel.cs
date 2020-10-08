using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class PerDiemRateModel
    {
        public int perdiem_id { get; set; }

        [Required(ErrorMessage = "Role is mandatory.")]
        [Display(Name = "Role")]
        [StringLength(50, ErrorMessage = "Name length cannot exceed 50 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string role { get; set; }

        [Required(ErrorMessage = "Value order is mandatory.")]
        [Display(Name = "Value")]
        public int value { get; set; }
    }
}