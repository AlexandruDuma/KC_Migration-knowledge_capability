using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class NotificationController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.NotificationModel model = new Models.NotificationModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    notification obj = context.notifications.FirstOrDefault(a => a.notification_id == id);
                    model.notification_id = obj.notification_id;
                    model.name = obj.name;
                    model.initiative_type_id = obj.initiative_type_id;
                    model.notification_trigger_id = obj.notification_trigger_id;
                    model.also_send_to = obj.also_send_to;
                    model.also_reply_to = obj.also_reply_to;
                    if (obj.oldest_year!=null) model.oldest_year = (int)obj.oldest_year;
                    if (obj.when_to_send != null) model.when_to_send = (int)obj.when_to_send;
                    model.frm = obj.frm;
                    model.subject = obj.subject;
                    model.body = obj.body;
                    //send to
                    List<notification_sendto> nst_list = context.notification_sendto.Where(a => a.notification_id == model.notification_id).ToList();
                    List<int> nst = new List<int>();
                    foreach (var nstItem in nst_list) { nst.Add(nstItem.notification_recipient_id); };
                    model.send_to = nst;
                    //reply to
                    List<notification_replyto> nrt_list = context.notification_replyto.Where(a => a.notification_id == model.notification_id).ToList();
                    List<int> nrt = new List<int>();
                    foreach (var nrtItem in nrt_list) { nrt.Add(nrtItem.notification_recipient_id); };
                    model.reply_to = nrt;
                }
            }
            //values for dropdowns
            model = LoadDropdownValues(model);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(KC.Models.NotificationModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    notification obj;
                    if (model.notification_id == 0) { obj = context.notifications.Create(); }
                    else { obj = context.notifications.FirstOrDefault(a => a.notification_id == model.notification_id); }

                    obj.name = model.name;
                    obj.initiative_type_id = model.initiative_type_id;
                    obj.notification_trigger_id = model.notification_trigger_id;
                    obj.also_send_to = model.also_send_to;
                    obj.also_reply_to = model.also_reply_to;
                    if (obj.oldest_year != null) obj.oldest_year = (int)obj.oldest_year;
                    if (obj.when_to_send != null) obj.when_to_send = (int)obj.when_to_send;
                    obj.frm = model.frm;
                    obj.subject = model.subject;
                    obj.body = model.body;

                    if (model.notification_id == 0)
                    {
                        context.notifications.Add(obj);
                        context.SaveChanges();
                    }

                    //find all current send to
                    List<notification_sendto> nst_list = context.notification_sendto.Where(a => a.notification_id == model.notification_id).ToList();
                    //delete the ones not used anymore
                    foreach (var nst in nst_list)
                    {
                        if ((model.send_to == null) || (!model.send_to.Contains(nst.notification_recipient_id)))
                        {                            
                            context.notification_sendto.Remove(nst);
                        }
                        else
                        {
                            model.send_to.Remove(nst.notification_recipient_id);      //found the recipient, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.send_to != null)
                    {
                        foreach (var nst in model.send_to)
                        {
                            notification_sendto notst = context.notification_sendto.Create();
                            notst.notification_id = obj.notification_id;
                            notst.notification_recipient_id = nst;
                            context.notification_sendto.Add(notst);
                        }
                    }
                    //find all current reply to
                    List<notification_replyto> nrt_list = context.notification_replyto.Where(a => a.notification_id == model.notification_id).ToList();
                    //delete the ones not used anymore
                    foreach (var nrt in nrt_list)
                    {
                        if ((model.reply_to == null) || (!model.reply_to.Contains(nrt.notification_recipient_id)))
                        {
                            context.notification_replyto.Remove(nrt);
                        }
                        else
                        {
                            model.reply_to.Remove(nrt.notification_recipient_id);      //found the recipient, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.reply_to != null)
                    {
                        foreach (var nrt in model.reply_to)
                        {
                            notification_replyto notrt = context.notification_replyto.Create();
                            notrt.notification_id = obj.notification_id;
                            notrt.notification_recipient_id = nrt;
                            context.notification_replyto.Add(notrt);
                        }
                    }

                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "notifications" });
            }
            else
            {
                return View(LoadDropdownValues(model));
            }
        }

        public Models.NotificationModel LoadDropdownValues(Models.NotificationModel model)
        {
            using (var context = new KandCEntities())
            {
                model.lstIniTypes = context.initiative_type.ToList();
                model.lstNotTriggers = context.notification_trigger.ToList();
                model.lstNotSendTo = context.notification_recipient.ToList();
                model.lstNotReplyTo = context.notification_recipient.Where(a => a.show_also_in_replyto).ToList();
            }

            return model;
        }

        public String Delete(int id)
        {
            try
            {
                using (var context = new KandCEntities())
                {
                    notification obj = context.notifications.FirstOrDefault(a => a.notification_id == id);
                    context.notifications.Remove(obj);
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