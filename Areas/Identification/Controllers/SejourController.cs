using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StrangerRecord.Models;
using StrangerRecord.Models.Entity;
using StrangerRecord.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.Identification.Controllers
{
    public class SejourController : Controller
    {
        // GET: Identification/Sejour
        public ActionResult Create(string id)
        {

            Carte carte = Service.DataProvider.FindCartById(id);

            if (carte == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.Requerant = carte.Identification;
            return View(new SejourViewModel
            {
                carteId = carte.id,
                IdentificationId = carte.identification_id,
                title = "Dernier deplacement [Nouveau]"
            });
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejour sejour = Service.DataProvider.FindSejourById(id);
            if (sejour == null)
            {
                return HttpNotFound();
            }

            SejourViewModel carteViewModel = new SejourViewModel
            {
                entityId = sejour.id,
                carteId = sejour.carte_id,
                IdentificationId = sejour.Carte.identification_id,
                MotifSejour = sejour.motif,
                DestinatiomAdresse = sejour.destination_adresse,
                DestinationPays = sejour.destination_pays,
                DestinationVille = sejour.destination_ville,

                DateDebutSejour = sejour.date_debut.ToString("dd/MM/yyyy"),
                DateFinSejour = sejour.date_fin.HasValue ? sejour.date_fin.Value.ToString("dd/MM/yyyy") : "",

                ProvenanceAdresse = sejour.provenance_adresse,
                ProvenancePays = sejour.provenance_pays,
                ProvenanceVille = sejour.provenance_ville,
                title = "Dernier deplacement [Modifier]"
            };

            ViewBag.Requerant = Service.DataProvider.FindCartById(sejour.carte_id); ;
            return View("Create", carteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SejourViewModel model)
        {
            Sejour sejour = Service.DataProvider.FindSejourById(model.entityId);

            if (ModelState.IsValid)
            {

                if (sejour == null)
                {
                    ApplicationUser user = HttpContext.GetOwinContext()
                  .GetUserManager<ApplicationUserManager>()
                  .FindById(HttpContext.User.Identity.GetUserId());

                    sejour = new Sejour
                    {
                        carte_id = model.carteId,
                        centre_id = user.centreId,
                        encodeur_id = user.Id
                    };
                }

                sejour.motif = model.MotifSejour;
                sejour.provenance_pays = model.ProvenancePays;
                sejour.provenance_ville = model.ProvenanceVille;
                sejour.provenance_adresse = model.ProvenanceAdresse;
                sejour.destination_ville = model.DestinationVille;
                sejour.destination_adresse = model.DestinatiomAdresse;
                sejour.destination_pays = model.DestinationPays;
                sejour.date_debut = DateTime.ParseExact(model.DateDebutSejour, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (!String.IsNullOrEmpty(model.DateFinSejour))
                    sejour.date_fin = DateTime.ParseExact(model.DateFinSejour, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Service.DataProvider.SaveSejour(sejour);

                return RedirectToAction("Details", "Carte", new { id = model.carteId, area = "Identification" });
            }

            Carte carte = Service.DataProvider.FindCartById(model.carteId);

            ViewBag.Requerant = carte.Identification;
            return View(model);
        }


    }
}