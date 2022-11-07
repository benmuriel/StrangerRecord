using StrangerRecord.Models;
using StrangerRecord.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.RapportIdentification.Controllers
{

   
    public class DefaultController : Controller
    {
        
        private IdentityContext context;
        // GET: RapportFrequentation/Default
        public ActionResult Index(string start, string end, string country,string mode ="enreg")
        {
            DateTime startdate = String.IsNullOrEmpty(start) ? DateTime.Today : DateTime.ParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime enddate = String.IsNullOrEmpty(end) ? DateTime.Today : DateTime.ParseExact(end, "dd/MM/yyyy", CultureInfo.InvariantCulture);
  
            List<Models.Entity.Carte> datas = new List<Models.Entity.Carte>();
            context = new IdentityContext(); 
                    switch (mode)
                    {
                        case "enreg":
                            datas = context.Cartes.Include("Identification").Include("Commune")
                            .Where(e => e.created_at >= startdate && e.Identification.created_at <= enddate && ((country != "" && e.Identification.pays_origine == country)|| country == ""))
                            .OrderByDescending(e => e.Identification.nom).ThenBy(e=>e.Identification.created_at).ToList();
                            break;
                        case "rdc":
                        datas = context.Cartes.Include("Identification").Include("Commune")
                            .Where(e => e.Identification.date_entree_rdc >= startdate && e.Identification.date_entree_rdc <= enddate && ((country != "" && e.Identification.pays_origine == country) || country == ""))
                            .OrderByDescending(e => e.Identification.nom).ThenBy(e => e.Identification.created_at).ToList();
                        break; 
                    }
                
           

            ViewBag.mode = mode;
            ViewBag.start = startdate.ToString("dd/MM/yyyy");
            ViewBag.end = enddate.ToString("dd/MM/yyyy");
            ViewBag.country = country;
            return View(datas);
        }
    }
}