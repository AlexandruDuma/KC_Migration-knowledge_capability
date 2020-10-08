using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class EndProductController : Controller
    {
        private KCHandler kCHandler;

        public ActionResult Edit(int id)
        {
            ViewBag.isValid = true;
            Models.EndProductModel model = new Models.EndProductModel();
            if (id != 0)
            {
                using (var context = new KandCEntities())
                {
                    end_product obj = context.end_product.FirstOrDefault(a => a.end_product_id == id);
                    model.end_product_id = obj.end_product_id;
                    model.display_order = obj.display_order;
                    model.name = obj.name;                    
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(KC.Models.EndProductModel model)
        {
            ViewBag.isValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                using (var context = new KandCEntities())
                {
                    if (model.end_product_id == 0)
                    {
                        //create
                        end_product obj = context.end_product.Create();
                        obj.display_order = model.display_order;
                        obj.name = model.name;                        
                        context.end_product.Add(obj);
                    }
                    else
                    {
                        //edit
                        end_product obj = context.end_product.FirstOrDefault(a => a.end_product_id == model.end_product_id);
                        obj.display_order = model.display_order;
                        obj.name = model.name;                        
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("ShowData", "Home", new { data = "endproducts" });
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
                    end_product obj = context.end_product.FirstOrDefault(a => a.end_product_id == id);
                    context.end_product.Remove(obj);
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