using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class ProjectStageController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.ProjectStageModel model = new Models.ProjectStageModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    project_stage obj = context.project_stage.FirstOrDefault(a => a.project_stage_id == id);
                    model.project_stage_id = obj.project_stage_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;
                    model.description = obj.description;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.ProjectStageModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.project_stage_id == 0)
                    {
                        //create
                        project_stage obj = context.project_stage.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        obj.description = model.description;
                        context.project_stage.Add(obj);
                    }
                    else
                    {
                        //edit
                        project_stage obj = context.project_stage.FirstOrDefault(a => a.project_stage_id == model.project_stage_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        obj.description = model.description;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "stages" });
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
                    project_stage obj = context.project_stage.FirstOrDefault(a => a.project_stage_id == id);
                    context.project_stage.Remove(obj);
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