using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class ClientInvolvementController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.ClientInvolvementModel model = new Models.ClientInvolvementModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    client_involvement obj = context.client_involvement.FirstOrDefault(a => a.client_involvement_id == id);
                    model.client_involvement_id = obj.client_involvement_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;                    
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.ClientInvolvementModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.client_involvement_id == 0)
                    {
                        //create
                        client_involvement obj = context.client_involvement.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;                        
                        context.client_involvement.Add(obj);
                    }
                    else
                    {
                        //edit
                        client_involvement obj = context.client_involvement.FirstOrDefault(a => a.client_involvement_id == model.client_involvement_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;                        
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "clientinv" });
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
                    client_involvement obj = context.client_involvement.FirstOrDefault(a => a.client_involvement_id == id);
                    context.client_involvement.Remove(obj);
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