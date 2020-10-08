using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class StaffingTypeController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.StaffingTypeModel model = new Models.StaffingTypeModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    staffing obj = context.staffings.FirstOrDefault(a => a.staffing_id == id);
                    model.staffing_id = obj.staffing_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;
                    model.description = obj.description;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.StaffingTypeModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.staffing_id == 0)
                    {
                        //create
                        staffing obj = context.staffings.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        obj.description = model.description;
                        context.staffings.Add(obj);
                    }
                    else
                    {
                        //edit
                        staffing obj = context.staffings.FirstOrDefault(a => a.staffing_id == model.staffing_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        obj.description = model.description;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "staffing" });
            }
            else
            {
                return View(model);
            }
        }
        public String Delete(int id)
        {
            try
            {
                using (var context = new KandCEntities())
                {
                    staffing obj = context.staffings.FirstOrDefault(a => a.staffing_id == id);
                    context.staffings.Remove(obj);
                    context.SaveChanges();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "Entry can't be deleted\n\n(" + ex.ToString() + ")!";
            }
            
        }
    }
}