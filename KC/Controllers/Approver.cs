using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class ApproverController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.ApproverModel model = new Models.ApproverModel();
            model.name = "";
            model.proxy_approvers = new List<string>();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    approver obj = context.approvers.FirstOrDefault(a => a.approver_id == id);
                    model.approver_id = obj.approver_id;
                    model.name = obj.name;                    
                    List<proxy_approver> pa_list = context.proxy_approver.Where(a => a.approver_id == model.approver_id).ToList();
                    foreach (var pa in pa_list)
                    {
                        model.proxy_approvers.Add(pa.name);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.ApproverModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    approver obj;
                    //create
                    if (model.approver_id == 0) { obj = context.approvers.Create(); }
                    //edit
                    else { obj = context.approvers.FirstOrDefault(a => a.approver_id == model.approver_id); }

                    //approver
                    obj.name = model.name;
                    if (model.approver_id == 0) 
                    { 
                        context.approvers.Add(obj);
                        context.SaveChanges();
                    }

                    //find all proxy approvers
                    List<proxy_approver> pa_list = context.proxy_approver.Where(a => a.approver_id == model.approver_id).ToList();
                    //delete the ones not used anymore
                    foreach (var pa in pa_list)
                    {
                        if ((model.proxy_approvers==null) || (!model.proxy_approvers.Contains(pa.name)))
                        {
                            //check if this proxy approver was used on initiatives before deletion!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            context.proxy_approver.Remove(pa);
                        }
                        else
                        {
                            model.proxy_approvers.Remove(pa.name);      //found the name, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.proxy_approvers != null)
                    {
                        foreach (var paname in model.proxy_approvers)
                        {
                            proxy_approver pa = context.proxy_approver.Create();
                            pa.approver_id = obj.approver_id;
                            pa.name = paname;
                            context.proxy_approver.Add(pa);
                        }
                    }

                    //save
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "approvers" });
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
                    approver obj = context.approvers.FirstOrDefault(a => a.approver_id == id);
                    context.approvers.Remove(obj);
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