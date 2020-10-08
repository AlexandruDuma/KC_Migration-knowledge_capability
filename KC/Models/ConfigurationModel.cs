using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KC.Models
{
    public class ConfigurationModel
    {
        public List<initiative_type> InitiativeTypes { get; set; }
        public List<project_stage> ProjectStages { get; set; }
        public List<taxonomy> Taxonomies { get; set; }
    }
}