using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class InitiativeController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Create()
        {
            ViewBag.isValid = true;
            Models.InitiativeCreateModel model = new Models.InitiativeCreateModel();
            using (var context = new KandCEntities())
            {
                model.initiativeTypeOptions = context.initiative_type.ToList();
                model.staffingOptions = context.staffings.OrderBy(a => a.display_order).ToList();
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.InitiativeModel model = new Models.InitiativeModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    //department obj = context.departments.FirstOrDefault(a => a.department_id == id);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.DepartmentModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    department obj;
                    obj = context.departments.FirstOrDefault(a => a.department_id == model.department_id); 
                    obj.allow_initiative_creation = model.allow_initiative_creation;
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "departments" });
            }
            else
            {
                return View(model);
            }
        }
    }
}