using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class ClientEngagementModel
    {
        public int client_engagement_id { get; set; }

        [Required(ErrorMessage = "Display order is mandatory.")]
        [Display(Name = "Display order")]
        public int display_order { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "Name length cannot exceed 250 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }
    }
}