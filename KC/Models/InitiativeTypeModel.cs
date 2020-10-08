using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Models
{
    public class InitiativeTypeModel
    {
        public int initiative_type_id { get; set; }

        [Required(ErrorMessage = "Code is mandatory.")]
        [Display(Name = "Code")]
        [StringLength(5, ErrorMessage = "Code length cannot exceed 5 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string code { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Name is mandatory.")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "Name length cannot exceed 250 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string name { get; set; }

        [Display(Name = "Short description")]
        [StringLength(500, ErrorMessage = "Short description length cannot exceed 500 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string short_description { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string description { get; set; }

        [Display(Name = "Allow creation")] 
        public bool allow_creation { get; set; }

        [Required(ErrorMessage = "Code is mandatory.")]
        [Display(Name = "Display color class")]
        [StringLength(50, ErrorMessage = "Display color class length cannot exceed 50 characters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string display_color_class { get; set; }

    }
}