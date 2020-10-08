using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class TeamRoleController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.TeamRoleModel model = new Models.TeamRoleModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    team_role obj = context.team_role.FirstOrDefault(a => a.team_role_id == id);
                    model.team_role_id = obj.team_role_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;
                    model.legacy = obj.legacy;
                    model.can_multiple = obj.can_multiple;
                    model.mandatory = obj.mandatory;                    
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.TeamRoleModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.team_role_id == 0)
                    {
                        //create
                        team_role obj = context.team_role.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        obj.legacy = model.legacy;
                        obj.can_multiple = model.can_multiple;
                        obj.mandatory = model.mandatory;                           
                        context.team_role.Add(obj);
                    }
                    else
                    {
                        //edit
                        team_role obj = context.team_role.FirstOrDefault(a => a.team_role_id == model.team_role_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        obj.legacy = model.legacy;
                        obj.can_multiple = model.can_multiple;
                        obj.mandatory = model.mandatory;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "roles" });
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
                    team_role obj = context.team_role.FirstOrDefault(a => a.team_role_id == id);
                    context.team_role.Remove(obj);
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