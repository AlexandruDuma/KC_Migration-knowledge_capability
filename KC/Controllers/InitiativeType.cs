using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class InitiativeTypeController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.InitiativeTypeModel model = new Models.InitiativeTypeModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    initiative_type obj = context.initiative_type.FirstOrDefault(a => a.initiative_type_id == id);
                    model.initiative_type_id = obj.initiative_type_id;
                    model.name = obj.name;
                    model.code = obj.code;
                    model.short_description = obj.short_description;
                    model.description = obj.description;
                    model.allow_creation = obj.allow_creation;
                    model.display_color_class = obj.display_color_class;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.InitiativeTypeModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.initiative_type_id == 0)
                    {
                        //create
                        initiative_type obj = context.initiative_type.Create();
                        obj.name = model.name;
                        obj.code = model.code;
                        if (model.short_description!=null) obj.short_description = model.short_description;
                        if (model.description!=null) obj.description = model.description;
                        obj.allow_creation = model.allow_creation;
                        obj.display_color_class = model.display_color_class;
                        context.initiative_type.Add(obj);
                    }
                    else
                    {
                        //edit
                        initiative_type obj = context.initiative_type.FirstOrDefault(a => a.initiative_type_id == model.initiative_type_id);
                        obj.name = model.name;
                        obj.code = model.code;
                        if (model.short_description != null) obj.short_description = model.short_description;
                        if (model.description != null) obj.description = model.description;
                        obj.allow_creation = model.allow_creation;
                        obj.display_color_class = model.display_color_class;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "initypes" });
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
                    initiative_type obj = context.initiative_type.FirstOrDefault(a => a.initiative_type_id == id);
                    context.initiative_type.Remove(obj);
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