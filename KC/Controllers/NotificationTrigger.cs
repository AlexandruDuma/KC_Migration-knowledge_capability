using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class NotificationTriggerController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.NotificationTriggerModel model = new Models.NotificationTriggerModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    notification_trigger obj = context.notification_trigger.FirstOrDefault(a => a.notification_trigger_id == id);
                    model.notification_trigger_id = obj.notification_trigger_id;
                    model.name = obj.name;                    
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.NotificationTriggerModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.notification_trigger_id == 0)
                    {
                        //create
                        notification_trigger obj = context.notification_trigger.Create();
                        obj.name = model.name;                        
                        context.notification_trigger.Add(obj);
                    }
                    else
                    {
                        //edit
                        notification_trigger obj = context.notification_trigger.FirstOrDefault(a => a.notification_trigger_id == model.notification_trigger_id);
                        obj.name = model.name;                        
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "notifitri" });
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
                    notification_trigger obj = context.notification_trigger.FirstOrDefault(a => a.notification_trigger_id == id);
                    context.notification_trigger.Remove(obj);
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