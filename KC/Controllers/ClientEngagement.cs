using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class ClientEngagementController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.ClientEngagementModel model = new Models.ClientEngagementModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    client_engagement obj = context.client_engagement.FirstOrDefault(a => a.client_engagement_id == id);
                    model.client_engagement_id = obj.client_engagement_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;                    
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.ClientEngagementModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.client_engagement_id == 0)
                    {
                        //create
                        client_engagement obj = context.client_engagement.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;                        
                        context.client_engagement.Add(obj);
                    }
                    else
                    {
                        //edit
                        client_engagement obj = context.client_engagement.FirstOrDefault(a => a.client_engagement_id == model.client_engagement_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;                        
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "clienteng" });
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
                    client_engagement obj = context.client_engagement.FirstOrDefault(a => a.client_engagement_id == id);
                    context.client_engagement.Remove(obj);
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