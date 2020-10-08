using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Models
{
    public class NotificationModel
    {
        public int notification_id { get; set; }

        [Display(Name = "For initiative of type")] 
        public int initiative_type_id { get; set; }

        [Display(Name = "Trigger for creation")] 
        public int notification_trigger_id { get; set; }

        [Display(Name = "Send To")]
        public List<int> send_to { get; set; }

        [Display(Name = "Reply To")]
        public List<int> reply_to { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "Name To length cannot exceed 250 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }

        [Display(Name = "Also Send To")]
        [StringLength(500, ErrorMessage = "Also Sent To length cannot exceed 500 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string also_send_to { get; set; }

        [Display(Name = "Also Reply To")]
        [StringLength(500, ErrorMessage = "Also Reply To length cannot exceed 500 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string also_reply_to { get; set; }
        
        [Display(Name = "When to send reminder")]
        public int when_to_send { get; set; }

        [Display(Name = "Oldest year for sending")]
        public int oldest_year { get; set; }

        [Required(ErrorMessage = "From is mandatory.")]
        [Display(Name = "From")]
        [StringLength(150, ErrorMessage = "From length cannot exceed 150 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string frm { get; set; }

        [Required(ErrorMessage = "Subject is mandatory.")]
        [Display(Name = "Subject")]
        [StringLength(150, ErrorMessage = "Subject length cannot exceed 150 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string subject { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Body is mandatory.")]
        [Display(Name = "Body")]        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string body { get; set; }

        public List<initiative_type> lstIniTypes { get; set; }
        public List<notification_trigger> lstNotTriggers { get; set; }
        public List<notification_recipient> lstNotSendTo { get; set; }
        public List<notification_recipient> lstNotReplyTo { get; set; }
    }
}