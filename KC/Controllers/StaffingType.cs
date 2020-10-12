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
                    List<initiative_type_staffing> its_list = context.initiative_type_staffing.Where(a => a.staffing_id == model.staffing_id).ToList();
                    List<int> its = new List<int>();
                    foreach (var istItem in its_list) { its.Add(istItem.initiative_type_id); };
                    model.forInitiatives = its;
                    model.initiativeTypeOptions = context.initiative_type.Where(a => a.allow_creation).ToList();
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
                    staffing obj;
                    if (model.staffing_id == 0)  obj = context.staffings.Create();
                    else obj = context.staffings.FirstOrDefault(a => a.staffing_id == model.staffing_id);

                    obj.display_order = model.display_order;
                    obj.name = model.name;
                    obj.description = model.description;

                    if (model.staffing_id == 0)
                    {
                        context.staffings.Add(obj);
                        context.SaveChanges();
                    }

                    //find all current initiative types
                    List<initiative_type_staffing> its_list = context.initiative_type_staffing.Where(a => a.staffing_id == model.staffing_id).ToList();
                    //delete the ones not used anymore
                    foreach (var its in its_list)
                    {
                        if ((model.forInitiatives == null) || (!model.forInitiatives.Contains(its.initiative_type_id)))
                        {
                            context.initiative_type_staffing.Remove(its);
                        }
                        else
                        {
                            model.forInitiatives.Remove(its.initiative_type_id);      //found the ini type, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.forInitiatives != null)
                    {
                        foreach (var its in model.forInitiatives)
                        {
                            initiative_type_staffing objITS = context.initiative_type_staffing.Create();
                            objITS.staffing_id = obj.staffing_id;
                            objITS.initiative_type_id = its;
                            context.initiative_type_staffing.Add(objITS);
                        }
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