using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace TaxonomyTree.Controllers
{
    public class AuthenticationController : Controller
    {
        protected bool ShowOntology()
        {
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            var groups = GetAdGroupsForUser(userName, yourDomain.Name);

            if (groups.Any(x => x.ToLower().Contains("ekam")) || userName == "ADS\\Alexandru Duma")
            {
                return true;
            }

            return false;
        }

        private List<string> GetAdGroupsForUser(string userName, string domainName = null)
        {
            var result = new List<string>();

            if (userName.Contains('\\') || userName.Contains('/'))
            {
                domainName = userName.Split(new char[] { '\\', '/' })[0];
                userName = userName.Split(new char[] { '\\', '/' })[1];
            }

            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domainName))
                using (UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, userName))
                using (var searcher = new DirectorySearcher(new DirectoryEntry("LDAP://" + domainContext.Name)))
                {
                    searcher.Filter = String.Format("(&(objectCategory=group)(member={0}))", user.DistinguishedName);
                    searcher.SearchScope = SearchScope.Subtree;
                    searcher.PropertiesToLoad.Add("cn");

                    foreach (SearchResult entry in searcher.FindAll())
                        if (entry.Properties.Contains("cn"))
                            result.Add(entry.Properties["cn"][0].ToString());
                }
            }
            return result;
        }
    }
}