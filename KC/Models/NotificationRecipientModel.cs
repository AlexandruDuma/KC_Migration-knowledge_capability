using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class NotificationRecipientModel
    {
        public int notification_recipient_id { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name length cannot exceed 50 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }

        [Display(Name = "Show Also in Reply To")]
        public bool show_also_in_replyto { get; set; }
    }
}