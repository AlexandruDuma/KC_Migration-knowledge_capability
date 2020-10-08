using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class NotificationTriggerModel
    {
        public int notification_trigger_id { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(150, ErrorMessage = "Name length cannot exceed 150 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }
    }
}