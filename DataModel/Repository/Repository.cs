using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class Repository
    {
        public List<taxonomy> GetTaxonomies()
        {
            using(var context = new KandCEntities())
            {
                return context.taxonomies.ToList();
            }
        }

        public List<initiative_type> GetInitiativeTypes()
        {
            using (var context = new KandCEntities())
            {
                return context.initiative_type.ToList();
            }
        }

        public List<project_stage> GetProjectStages()
        {
            using (var context = new KandCEntities())
            {
                return context.project_stage.ToList();
            }
        }
    }
}
