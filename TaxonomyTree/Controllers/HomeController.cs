using DataModel.Repository;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TaxonomyTree.Models;
using TaxonomyTree.Services;

namespace TaxonomyTree.Controllers
{
    public class HomeController : AuthenticationController
    {
        private HomeService homeService;

        public HomeController()
        {
            homeService = new HomeService();
        }

        public ActionResult Index()
        {
            //homeService.ImportTaxonomies();
            //homeService.ImportEngageTerms();
            var model = new HomeModel();

            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ViewBag.uname = userName;

            /*
            var domainName = "";
            if (userName.Contains('\\') || userName.Contains('/'))
            {
                domainName = userName.Split(new char[] { '\\', '/' })[0];
                userName = userName.Split(new char[] { '\\', '/' })[1];
            }

            using (HostingEnvironment.Impersonate())
            { 
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domainName);

                ViewBag.uname = userName;
                ViewBag.domain = domainName + " - " + (domainContext == null ? "NULL" : domainContext.ConnectedServer);
            }
            */

            return View(model);
        }

        [HttpPost]
        public JsonResult GetJSONTaxonomies()
        {
            try 
            {
                using (var context = new KandCEntities())
                {
                    if (ShowOntology()) return Json(context.taxonomies.OrderBy(a => a.magic_tree).ToList());
                    else return Json(context.taxonomies.Where(a => a.is_practice == true).OrderBy(b => b.magic_tree).ToList());
                }
            }
            catch (Exception eh)
            {
                return Json(eh.Message + " - " + (eh.InnerException == null ? "":eh.InnerException.Message) + " - " + eh.StackTrace);
            }
        }

        [HttpPost]
        public JsonResult GetJSONEngageTerms()
        {
            using (var context = new KandCEntities())
            {
                return Json(context.engageterms.OrderBy(a => a.magic_tree).ToList());
            }
        }

        [HttpPost]
        public JsonResult DownloadJSONTaxonomies(string token)
        {
            return Json(homeService.ImportTaxonomies(token));
        }

        [HttpPost]
        public JsonResult DownloadJSONEngageTerms(string token) 
        {            
            return Json(homeService.ImportEngageTerms(token));
        }
    }
}