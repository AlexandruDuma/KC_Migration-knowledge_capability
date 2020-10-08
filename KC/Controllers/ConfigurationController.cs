using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class ConfigurationController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult ShowInitiativeTypes()
        {
            kCHandler = new KCHandler();

            Models.ConfigurationModel model = new Models.ConfigurationModel();
            model.InitiativeTypes = kCHandler.GetInitiativeTypes();
            
            return View(model);
        }

        public ActionResult ShowProjectStages()
        {
            kCHandler = new KCHandler();

            Models.ConfigurationModel model = new Models.ConfigurationModel();
            model.ProjectStages = kCHandler.GetProjectStages();

            return View(model);
        }
    }
}