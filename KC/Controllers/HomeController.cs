using CommonObjects;
using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowData()
        {
            return View();
        }

        public JsonResult GetJSONData(string key)
        {
            try
            {
                using (var context = new KandCEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    if (key == "initypes") return Json(new { data = context.initiative_type.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "clientinv") return Json(new { data = context.client_involvement.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "external3p") return Json(new { data = context.external_3rd_party.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "perdiems") return Json(new { data = context.perdiems.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "staffing") return Json(new { data = context.staffings.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "stages") return Json(new { data = context.project_stage.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "roles") return Json(new { data = context.team_role.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "endproducts") return Json(new { data = context.end_product.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "clienteng") return Json(new { data = context.client_engagement.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "approvers") return Json(new { data = context.approvers.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "notifirec") return Json(new { data = context.notification_recipient.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "notifitri") return Json(new { data = context.notification_trigger.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "notifications") return Json(new { data = context.notifications.ToList() }, JsonRequestBehavior.AllowGet);
                    if (key == "departments")
                    {
                        //string sir = new KCHandler().ImportDepartments("6pjAWau1oQlV4Vv8GbQjvcJcLj1j");
                        return Json(new { data = context.departments.ToList() }, JsonRequestBehavior.AllowGet);
                    }
                    if (key == "owners") 
                    {
                        var lstOwners = context.owners.ToList();
                        List<ownerc> lstOW = new List<ownerc>();
                        foreach (var ow in lstOwners) 
                        {
                            ownerc o = new ownerc(ow.owner_id, ow.name);
                            lstOW.Add(o);
                        }
                        return Json(new { data = lstOW }, JsonRequestBehavior.AllowGet);
                    }
                    if (key == "approvers.all") 
                    {
                        var lstApprovers = context.approvers.ToList();
                        List<s2Item> lstS2 = new List<s2Item>();
                        foreach (var apr in lstApprovers)
                        {
                            s2Item s2 = new s2Item(apr.approver_id, apr.name);
                            lstS2.Add(s2);
                        }
                        return Json(lstS2, JsonRequestBehavior.AllowGet);
                    }
                    if (key == "departments.all")
                    {
                        var lstDepartments = context.departments.ToList();
                        List<s2Item> lstS2 = new List<s2Item>();
                        foreach (var dep in lstDepartments)
                        {
                            s2Item s2 = new s2Item(dep.department_id, dep.code + " - " + dep.name);
                            lstS2.Add(s2);
                        }
                        return Json(lstS2, JsonRequestBehavior.AllowGet);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return Json(new { ex.InnerException }, JsonRequestBehavior.AllowGet);
            }
        }

        public class ownerc 
        { 
            public int owner_id { get; set; }
            public string name { get; set; }

            public ownerc(int id, string name) 
            {
                this.owner_id = id;
                this.name = name;
            }
        }
        public class s2Item
        {
            public int id { get; set; }
            public string text { get; set; }

            public s2Item(int id, string text)
            {
                this.id = id;
                this.text = text;
            }
        }


        public JsonResult GetActiveDirectoryPersons(string term)
        {
            var result = new List<select2Name>();

            if ((term!=null) && (term.Length > 2))
            {
                String myDomain = null;
                var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                if (userName.Contains('\\') || userName.Contains('/'))
                {
                    myDomain = userName.Split(new char[] { '\\', '/' })[0];
                    userName = userName.Split(new char[] { '\\', '/' })[1];
                }

                using (PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, myDomain))
                using (UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, userName))
                using (var searcher = new DirectorySearcher(new DirectoryEntry("LDAP://" + domainContext.Name)))
                {
                    searcher.Filter = String.Format("(&(&(objectClass=user)(objectClass=person)(cn={0}*)))", term);
                    searcher.SearchScope = SearchScope.Subtree;
                    searcher.PropertiesToLoad.Add("cn");
                    searcher.SizeLimit = 20;

                    foreach (SearchResult entry in searcher.FindAll())
                        if (entry.Properties.Contains("cn"))
                        {
                            select2Name nam = new select2Name();
                            nam.id = entry.Properties["cn"][0].ToString();
                            nam.text = entry.Properties["cn"][0].ToString();
                            result.Add(nam);
                        }
                }
            }

            return Json(new { results = result }, JsonRequestBehavior.AllowGet);
        }

        public class select2Name
        {
            public string id { get; set; }  //abbreviated name
            public string text { get; set; }  //common name
        }
    }
}