using StrangerRecord.Models;
using StrangerRecord.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.AlerteExpiration.Controllers
{
    public class DefaultController : Controller
    {
        // GET: AlerteExpiration/ListeAlerteExpiration
        public ActionResult Index(string echeance, string dateDebut, string dateFin, string type)
        {
            /*
            using (var context = new IdentityContext())
            {
                var cartes = context.Cartes.ToList();
                return View(cartes);
            }
            //return View();
            */
            using (var context = new IdentityContext())
            {
                List<Carte> datas = new List<Carte>();
                //dateDebut = Request.QueryString["dateDebut"];
                //dateFin = Request.QueryString["dateFin"];
                //type = Request.QueryString["type"];
                if (!string.IsNullOrEmpty(dateDebut) && !string.IsNullOrEmpty(dateFin) && !string.IsNullOrEmpty(type))
                {
                    //q = q.Trim().ToLower();
                    DateTime DateDebut = DateTime.ParseExact(dateDebut,"dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime DateFin = DateTime.ParseExact(dateFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    switch (type)
                    {
                        case "Carte":
                            datas = context.Cartes.Include("Identification").Where(e => e.date_expiration >= DateDebut && e.date_expiration <= DateFin).OrderByDescending(e => e.created_at).ToList();
                            break;
                        case "Passport":
                            datas = context.Cartes.Include("Identification")
                                .Where(e => e.passeport_exp_date >= DateDebut && e.passeport_exp_date <= DateFin)
                                .OrderByDescending(e => e.created_at).ToList();
                            break;
                        case "Visa":
                            datas = context.Cartes.Include("Identification")
                                .Where(e => e.visa_exp_date >= DateDebut && e.visa_exp_date <= DateFin).OrderByDescending(e => e.created_at).ToList();
                            break;
                    }
                }

                ViewBag.echeance = echeance;
                ViewBag.dateDebut = dateDebut;
                ViewBag.dateFin = dateFin;
                ViewBag.type = type;
                return View(datas);
            }
            
        }
    }
}