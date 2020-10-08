using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KC.Controllers
{
    public class OwnerController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.OwnerModel model = new Models.OwnerModel();
            model.name = "";
            model.owner_contacts = new List<string>();
            model.owner_approvers = new List<string>();
            model.owner_departments = new List<string>();
            using (var context = new KandCEntities())
            {
                if (id != 0)
                {
                    owner obj = context.owners.FirstOrDefault(a => a.owner_id == id);
                    model.owner_id = obj.owner_id;
                    model.name = obj.name;
                    if (obj.parent_id != null) model.parent_id = (int)obj.parent_id;

                    List<owner_contact> c_list = context.owner_contact.Where(a => a.owner_id == model.owner_id).ToList();
                    foreach (var c in c_list) { model.owner_contacts.Add(c.name); }

                    List<owner_approver> a_list = context.owner_approver.Where(a => a.owner_id == model.owner_id).ToList();
                    foreach (var a in a_list) { model.owner_approvers.Add(a.approver_id.ToString() + "#" + a.approver.name); }

                    List<owner_department> d_list = context.owner_department.Where(a => a.owner_id == model.owner_id).ToList();
                    foreach (var d in d_list) { model.owner_departments.Add(d.department_id.ToString() + "#" + d.department.code + " - " + d.department.name); }
                }

                model.lstPossibleParentPractices = context.owners.Where(a => a.parent_id == null && a.owner_id != model.owner_id).ToList();                
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.OwnerModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    owner obj;
                    //create
                    if (model.owner_id == 0) { obj = context.owners.Create(); }
                    //edit
                    else { obj = context.owners.FirstOrDefault(a => a.owner_id == model.owner_id); }

                    //owner
                    obj.name = model.name;
                    if (model.parent_id == 0) obj.parent_id = null;
                    else obj.parent_id = model.parent_id;

                    if (model.owner_id == 0) 
                    { 
                        context.owners.Add(obj);
                        context.SaveChanges();
                    }

                    //find all contacts
                    List<owner_contact> c_list = context.owner_contact.Where(a => a.owner_id == model.owner_id).ToList();
                    //delete the ones not used anymore
                    foreach (var c in c_list)
                    {
                        if ((model.owner_contacts == null) || (!model.owner_contacts.Contains(c.name)))
                        {
                            //check if this proxy approver was used on initiatives before deletion!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            context.owner_contact.Remove(c);
                        }
                        else
                        {
                            model.owner_contacts.Remove(c.name);      //found the name, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.owner_contacts != null)
                    {
                        foreach (var cname in model.owner_contacts)
                        {
                            owner_contact oc = context.owner_contact.Create();
                            oc.owner_id = obj.owner_id;
                            oc.name = cname;
                            context.owner_contact.Add(oc);
                        }
                    }

                    //find all approvers
                    List<owner_approver> a_list = context.owner_approver.Where(a => a.owner_id == model.owner_id).ToList();
                    //delete the ones not used anymore
                    foreach (var a in a_list)
                    {
                        if ((model.owner_approvers == null) || (!model.owner_approvers.Contains(a.approver_id.ToString())))
                        {
                            //check if this proxy approver was used on initiatives before deletion!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            context.owner_approver.Remove(a);
                        }
                        else
                        {
                            model.owner_approvers.Remove(a.approver_id.ToString());      //found the name, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.owner_approvers != null)
                    {
                        foreach (string aid in model.owner_approvers)
                        {
                            owner_approver oa = context.owner_approver.Create();
                            oa.owner_id = obj.owner_id;
                            oa.approver_id = Int32.Parse(aid);
                            context.owner_approver.Add(oa);
                        }
                    }

                    //find all departments
                    List<owner_department> d_list = context.owner_department.Where(a => a.owner_id == model.owner_id).ToList();
                    //delete the ones not used anymore
                    foreach (var d in d_list)
                    {
                        if ((model.owner_departments == null) || (!model.owner_departments.Contains(d.department_id.ToString())))
                        {
                            //check if this proxy approver was used on initiatives before deletion!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            context.owner_department.Remove(d);
                        }
                        else
                        {
                            model.owner_departments.Remove(d.department_id.ToString());      //found the name, so we don't need to add it again
                        }
                    }
                    //add the new ones - existing ones were removed from the list at the previous step
                    if (model.owner_departments != null)
                    {
                        foreach (string did in model.owner_departments)
                        {
                            owner_department od = context.owner_department.Create();
                            od.owner_id = obj.owner_id;
                            od.department_id = Int32.Parse(did);
                            context.owner_department.Add(od);
                        }
                    }

                    //save
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "owners" });
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
                    owner obj = context.owners.FirstOrDefault(a => a.owner_id == id);
                    context.owners.Remove(obj);
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