using CommonObjects;
using DataModel.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace TaxonomyTree.Services
{
    public class HomeService
    {
        private KCHandler kCHandler;

        public HomeService()
        {
            kCHandler = new KCHandler();
        }

        internal List<taxonomy> GetTaxonomies()
        {
            return kCHandler.GetTaxonomies();
        }

        internal string ImportTaxonomies(string token)
        {
            try
            {
                var js = kCHandler.ImportTaxonomies(token);
                return js;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        internal string ImportEngageTerms(string token)
        {
            try
            {
                var js = kCHandler.ImportEngageTerms(token);
                return js;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}