using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class InitiativeModel
    { 
    
    }
    public class InitiativeCreateModel
    {
        public String initiative_type { get; set; }

        public String staffing { get; set; }

        public List<staffing> staffingOptions { get; set; }
        public List<initiative_type> initiativeTypeOptions { get; set; }
    }
}