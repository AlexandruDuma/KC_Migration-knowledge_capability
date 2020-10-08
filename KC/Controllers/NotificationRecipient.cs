using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class NotificationRecipientController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.NotificationRecipientModel model = new Models.NotificationRecipientModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    notification_recipient obj = context.notification_recipient.FirstOrDefault(a => a.notification_recipient_id == id);
                    model.notification_recipient_id = obj.notification_recipient_id;                    
                    model.name = obj.name;
                    model.show_also_in_replyto = obj.show_also_in_replyto;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.NotificationRecipientModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.notification_recipient_id == 0)
                    {
                        //create
                        notification_recipient obj = context.notification_recipient.Create();                        
                        obj.name = model.name;
                        obj.show_also_in_replyto = model.show_also_in_replyto;
                        context.notification_recipient.Add(obj);
                    }
                    else
                    {
                        //edit
                        notification_recipient obj = context.notification_recipient.FirstOrDefault(a => a.notification_recipient_id == model.notification_recipient_id);                        
                        obj.name = model.name;
                        obj.show_also_in_replyto = model.show_also_in_replyto;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "notifirec" });
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
                    notification_recipient obj = context.notification_recipient.FirstOrDefault(a => a.notification_recipient_id == id);
                    context.notification_recipient.Remove(obj);
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