using DataModel.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonObjects
{
    public class KCHandler
    {
        public List<taxonomy> GetTaxonomies()
        {
            return new Repository().GetTaxonomies();
        }

        public List<initiative_type> GetInitiativeTypes()
        {
            return new Repository().GetInitiativeTypes();
        }

        public List<project_stage> GetProjectStages()
        {
            return new Repository().GetProjectStages();
        }

        public string ImportTaxonomies(string token)
        {
            var js = 0;
            
            try
            {
                var context = new KandCEntities();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [taxonomy]");

                var json_data = string.Empty;                
                string url = "http://api.mckinsey.com/v3/taxonomies?q=*&wt=json&start=0&rows=6000";

                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Bearer " + token);

                    json_data = client.DownloadString(url);

                    JObject joResponse = JObject.Parse(json_data);

                    using (context)
                    {
                        foreach (var term in joResponse["response"]["docs"])
                        {
                            js++;
                            if (term["uri"] != null)
                            {
                                taxonomy tax = new taxonomy();
                                tax.taxonomy_id = Int32.Parse(term["uri"].ToString().Split('/').Last());
                                tax.text = term["prefLabel"] != null ? term["prefLabel"].ToString() : "";
                                if (term["broaders"] != null) tax.parent_taxonomy_id = Int32.Parse(term["broaders"][0].ToString().Split('/').Last());
                                if (term["definitions"] != null) tax.definition = term["definitions"][0].ToString() ;
                                if (term["lineage"] != null) tax.magic_tree = term["lineage"][0].ToString();
                                if (term["isPracticeFlag"] != null) tax.is_practice = term["isPracticeFlag"][0].ToString() == "Y";                                
                                if (term["properties_http://amdc-poolpart01.ads.mckinsey.com/FTA-Data/Archived-Flag"] != null) tax.archive = term["properties_http://amdc-poolpart01.ads.mckinsey.com/FTA-Data/Archived-Flag"][0].ToString() == "Y";
                                if (term["properties_http://amdc-poolpart01.ads.mckinsey.com/FTA-Data/Term-ID"] != null) tax.legacy_id = Int32.Parse(term["properties_http://amdc-poolpart01.ads.mckinsey.com/FTA-Data/Term-ID"][0].ToString());
                                if (term["properties_http://amdc-poolpart01.ads.mckinsey.com/FTA-Data/Parent-Term-ID"] != null) tax.legacy_parent_id = Int32.Parse(term["properties_http://amdc-poolpart01.ads.mckinsey.com/FTA-Data/Parent-Term-ID"][0].ToString());

                                if (tax.magic_tree == null)
                                {
                                    tax.magic_tree = "Taxonomy Root//" + tax.text;
                                }
                                else
                                {
                                    var tree = tax.magic_tree.Split(new string[] { "//" }, StringSplitOptions.None);
                                    var topCategory = Int32.Parse(term["lineageConceptSchema"][0].ToString().Split('/').Last());
                                    if (topCategory == 0) tree[0] = "Taxonomy Root//Industry";
                                    if (topCategory == 1654) tree[0] = "Taxonomy Root//Functional";
                                    if (topCategory == 1301) tree[0] = "Taxonomy Root//Geography";
                                    tax.magic_tree = string.Join("//", tree);

                                    if (tax.parent_taxonomy_id == null) 
                                    {
                                        if ((tax.taxonomy_id != 0) && (tax.taxonomy_id != 1654) && (tax.taxonomy_id != 1301))
                                        {
                                            tax.parent_taxonomy_id = topCategory;
                                        }
                                    }
                                }

                                context.taxonomies.Add(tax);
                            }
                        }
                        
                        context.SaveChanges();
                    }
                }
                return js.ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return js.ToString();
            }
        }

        public string ImportEngageTerms(string token)
        {
            var js = 0;

            try
            {
                
                var context = new KandCEntities();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [engageterm]");

                var json_data = string.Empty;                
                string url = "http://api.mckinsey.com/engage/practices";

                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Bearer " + token);

                    json_data = client.DownloadString(url);

                    JObject joResponse = JObject.Parse(json_data);

                    using (context)
                    {
                        //industry
                        foreach (JObject term in joResponse["practiceIndustries"])
                        {                            
                            if (term["parentTrace"].ToArray().Length == 0)
                            {
                                engageterm et = NewEngageTerm(term, "Industry//" + term["prefLabel"].ToString());
                                context.engageterms.Add(et);
                                js++;

                                foreach (JObject narrowMatch in term["narrowMatch"])
                                {
                                    context.engageterms.Add(NewEngageTerm(narrowMatch, "Industry//" + et.label + "//" + narrowMatch["prefLabel"].ToString()));
                                    js++;
                                }
                                foreach (JObject exactMatch in term["exactMatch"])
                                {
                                    context.engageterms.Add(NewEngageTerm(exactMatch, "Industry//" + et.label + "//" + exactMatch["prefLabel"].ToString()));
                                    js++;
                                }
                            }
                            else
                            {
                                foreach (var parentLabel in term["parentTrace"])
                                {
                                    engageterm et = NewEngageTerm(term, "Industry//" + parentLabel.ToString() + "//" + term["prefLabel"].ToString());
                                    context.engageterms.Add(et);
                                    js++;

                                    foreach (JObject narrowMatch in term["narrowMatch"])
                                    {
                                        context.engageterms.Add(NewEngageTerm(narrowMatch, "Industry//" + parentLabel.ToString() + "//" + et.label + "//" + narrowMatch["prefLabel"].ToString()));
                                        js++;
                                    }
                                    foreach (JObject exactMatch in term["exactMatch"])
                                    {
                                        context.engageterms.Add(NewEngageTerm(exactMatch, "Industry//" + parentLabel.ToString() + "//" + et.label + "//" + exactMatch["prefLabel"].ToString()));
                                        js++;
                                    }
                                }
                            }
                        }

                        //function
                        foreach (JObject term in joResponse["practiceFunctions"])
                        {
                            if (term["parentTrace"].ToArray().Length == 0)
                            {
                                engageterm et = NewEngageTerm(term, "Functional//" + term["prefLabel"].ToString());
                                context.engageterms.Add(et);
                                js++;

                                foreach (JObject narrowMatch in term["narrowMatch"])
                                {
                                    context.engageterms.Add(NewEngageTerm(narrowMatch, "Functional//" + et.label + "//" + narrowMatch["prefLabel"].ToString()));
                                    js++;
                                }
                                /*
                                foreach (JObject exactMatch in term["exactMatch"])
                                {
                                    context.engageterms.Add(NewEngageTerm(exactMatch, "Functional//" + et.label + "//" + exactMatch["prefLabel"].ToString()));
                                    js++;
                                }
                                */
                            }
                            else
                            {
                                foreach (var parentLabel in term["parentTrace"])
                                {
                                    engageterm et = NewEngageTerm(term, "Functional//" + parentLabel.ToString() + "//" + term["prefLabel"].ToString());
                                    context.engageterms.Add(et);
                                    js++;

                                    foreach (JObject narrowMatch in term["narrowMatch"])
                                    {
                                        context.engageterms.Add(NewEngageTerm(narrowMatch, "Functional//" + parentLabel.ToString() + "//" + et.label + "//" + narrowMatch["prefLabel"].ToString()));
                                        js++;
                                    }
                                    /*
                                    foreach (JObject exactMatch in term["exactMatch"])
                                    {
                                        context.engageterms.Add(NewEngageTerm(exactMatch, "Functional//" + parentLabel.ToString() + "//" + et.label + "//" + exactMatch["prefLabel"].ToString()));
                                        js++;
                                    }
                                    */
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }
                
                return js.ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return js.ToString();
            }
        }

        public engageterm NewEngageTerm(JObject term, string mt)
        {
            engageterm et = new engageterm
            {
                class_code = Int32.Parse(term["classCode"].ToString()),
                label = term["prefLabel"].ToString().Replace("Core Technology", "McKinsey Technology"),
                selectable = term["selectable"][0].ToString() == "true",
                category = term["category"] == null ? "" : term["category"][0].ToString(),
                magic_tree = mt.Replace("Core Technology", "McKinsey Technology")
            };

            return et;
        }

        public string ImportDepartments(string token)
        {
            var js = 0;

            try
            {
                var context = new KandCEntities();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [department]");

                var json_data = string.Empty;
                string url = "http://api.mckinsey.com/v2/departments";

                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Bearer " + token);

                    json_data = client.DownloadString(url);

                    JObject joResponse = JObject.Parse(json_data);

                    using (context)
                    {
                        foreach (var term in joResponse["departments"])
                        {
                            js++;
                            //if (js <= 1000)
                            {
                                if (term["gocDepartmentCode"].ToString() != "NA")
                                {
                                    department dep = new department();
                                    dep.code = Int32.Parse(term["gocDepartmentCode"].ToString());
                                    dep.name = term["gocDepartmentName"].ToString();
                                    dep.allow_initiative_creation = true;

                                    context.departments.Add(dep);
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }
                return js.ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return js.ToString();
            }



        }
    }
}
