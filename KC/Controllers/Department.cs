using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class DepartmentController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.DepartmentModel model = new Models.DepartmentModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    department obj = context.departments.FirstOrDefault(a => a.department_id == id);
                    model.department_id = obj.department_id;
                    model.code = obj.code;
                    model.name = obj.name;
                    model.allow_initiative_creation = obj.allow_initiative_creation;
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