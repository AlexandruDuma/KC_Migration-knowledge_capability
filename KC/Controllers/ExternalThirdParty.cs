using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class ExternalThirdPartyController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.ExternalThirdPartyModel model = new Models.ExternalThirdPartyModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    external_3rd_party obj = context.external_3rd_party.FirstOrDefault(a => a.external_3rd_party_id == id);
                    model.external_3rd_party_id = obj.external_3rd_party_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.ExternalThirdPartyModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.external_3rd_party_id == 0)
                    {
                        //create
                        external_3rd_party obj = context.external_3rd_party.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                        context.external_3rd_party.Add(obj);
                    }
                    else
                    {
                        //edit
                        external_3rd_party obj = context.external_3rd_party.FirstOrDefault(a => a.external_3rd_party_id == model.external_3rd_party_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "external3p" });
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
                    external_3rd_party obj = context.external_3rd_party.FirstOrDefault(a => a.external_3rd_party_id == id);
                    context.external_3rd_party.Remove(obj);
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