using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StrangerRecord.Models;
using StrangerRecord.Models.Entity;
using StrangerRecord.Models.ViewModel;

namespace StrangerRecord.Areas.Identification.Controllers
{
    public class RecordController : Controller
    {
        IdentityContext db;
        public RecordController()
        {
            db = new IdentityContext();
        }

        // GET: Identification/Record/Details/5
        public ActionResult Details(string id)
        {
            Models.Entity.Identification identification = db.Identifications.Find(id);
            return View(identification);
        }

        [HttpGet]
        // GET: Identification/Record/Details/5
        public ActionResult Edit(string id)
        {
            Models.Entity.Identification identification = db.Identifications.Find(id);
            if (identification == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(new EditIdentificationViewModel
            {
                ContactMail = identification.contact_mail,
                ContactTelephone = identification.contact_telephone,
                dateEntreeRdc = identification.date_entree_rdc,
                date_naissance = identification.date_naissance,
                genre = identification.genre,
                id = identification.id,
                nom = identification.nom,
                paysOrigine = identification.pays_origine,
                postenom = identification.postenom,
                prenom = identification.prenom,
                profession = identification.profession,
                villeOrigine = identification.ville_pays_origine
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditIdentificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Entity.Identification identification = db.Identifications.Find(model.id);

                identification.contact_mail = model.ContactMail;
                identification.contact_telephone = model.ContactTelephone;
                identification.date_entree_rdc = model.dateEntreeRdc;
                identification.date_naissance = model.date_naissance;
                identification.genre = model.genre;
                identification.nom = model.nom;
                identification.pays_origine = model.paysOrigine;
                identification.postenom = model.postenom;
                identification.prenom = model.prenom;
                identification.profession = model.profession;
                identification.ville_pays_origine = model.villeOrigine;

                db.SaveChanges();
                return RedirectToAction("Details", new { id = identification.id, area = "Identification" });
            }
            return View(model);
        }

        [HttpGet]
        // GET: Identification/Record/Create
        public ActionResult Create()
        {
            IdentificationViewModel model = new IdentificationViewModel { DateExpirationCarte = DateTime.Today.AddYears(1) };
            return View(model);
        }

        // POST: Identification/Record/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.

        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(HttpContext.User.Identity.GetUserId());

                Models.Entity.Identification identification = new Models.Entity.Identification
                {
                    id = model.id,
                    created_at = DateTime.Now,
                    nom = model.nom,
                    postenom = model.postenom,
                    prenom = model.prenom,
                    date_naissance = model.date_naissance,
                    genre = model.genre,
                    profession = model.profession,
                    date_entree_rdc = model.dateEntreeRdc,
                    pays_origine = model.paysOrigine,
                    ville_pays_origine = model.villeOrigine,
                    contact_mail = model.ContactMail,
                    contact_telephone = model.ContactTelephone,
                    centre_id = user.centreId,
                    encodeur_id = user.Id

                };
                Carte carte = new Models.Entity.Carte
                {
                    numero = Service.DataProvider.GetNewCarteNumero(), // generer un numero de carte sur 10 caracteres
                    date_delivrance = DateTime.Now,
                    adresse_avenue = model.AdresseAvenue,
                    adresse_numero = model.AdresseNumero,
                    adresse_quartier = model.AdresseQuartier,
                    adresse_commune_id = model.AdresseCommuneId,
                    visa_exp_date = model.DateExpirationVisa,
                    visa_numero = model.NumeroVisa,
                    type_visa_id = model.TypeVisa,
                    type_passeport_id = model.TypePassport,
                    passeport_exp_date = model.DateExpirationPassePort,
                    passeport_numero = model.NumeroPassePort,
                    date_expiration = model.DateExpirationCarte,

                    identification_id = identification.id,
                    encodeur_id = user.Id,
                    centre_id = user.centreId,
                    // completer la date de delivrance  et la date d' expiration au moment de l impression de la carte

                };
                CentreEnregistrement centre = db.CentreEnregistrements.Include("ville").FirstOrDefault(e => e.id == user.centreId);
                carte.Sejours.Add(new Sejour
                {
                    carte_id = carte.id,
                    motif = model.MotifSejour,
                    provenance_pays = model.paysOrigine,
                    provenance_ville = model.villeOrigine,
                    destination_ville = centre.ville.designation,
                    destination_pays = "RDC",
                    date_debut = model.DateDebutSejour,
                    date_fin = model.DateFinSejour,
                    centre_id = user.centreId,
                    encodeur_id = user.Id
                });
                identification.Cartes.Add(carte);
                db.Identifications.Add(identification);

                db.SaveChanges();
                return RedirectToAction("Details", new { id = identification.id, area = "Identification" });

            }

            return View(model);
        }


    }
}
