using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class PerDiemRateController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.PerDiemRateModel model = new Models.PerDiemRateModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    perdiem obj = context.perdiems.FirstOrDefault(a => a.perdiem_id == id);
                    model.role = obj.role;
                    model.value = obj.value;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.PerDiemRateModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.perdiem_id == 0)
                    {
                        //create
                        perdiem obj = context.perdiems.Create();
                        obj.role = model.role;
                        obj.value = model.value;                        
                        context.perdiems.Add(obj);
                    }
                    else
                    {
                        //edit
                        perdiem obj = context.perdiems.FirstOrDefault(a => a.perdiem_id == model.perdiem_id);
                        obj.role = model.role;
                        obj.value = model.value;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "perdiems" });
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
                    perdiem obj = context.perdiems.FirstOrDefault(a => a.perdiem_id == id);
                    context.perdiems.Remove(obj);
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