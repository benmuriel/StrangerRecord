using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StrangerRecord.Models;
using StrangerRecord.Models.Entity;
using StrangerRecord.Models.ViewModel;

namespace StrangerRecord.Areas.Identification.Controllers
{
    public class CarteController : Controller
    {



        // GET: Identification/Carte/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = Service.DataProvider.FindCartById(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            TempData["currentCarteId"] = id;
            return View(carte);
        }
        public ActionResult Apercu(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = Service.DataProvider.FindCartById(id);
            if (carte == null)
            {
                return HttpNotFound();
            }


            return View(carte);
        }

        public ActionResult UploadPicture(string id)
        {
            Carte carte = Service.DataProvider.FindCartById(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(new UploadPictureViewModel { carteId = carte.id,  Name = carte.Identification.ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPicture(string id,UploadPictureViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                Carte carte = Service.DataProvider.FindCartById(model.carteId);
                if (carte == null)
                {
                    return HttpNotFound();
                }
                carte.picture_format = model.Picture.ContentType;
                var uploadedFiile = new byte[model.Picture.InputStream.Length];
                model.Picture.InputStream.Read(uploadedFiile, 0, uploadedFiile.Length); 
                carte.picture = uploadedFiile;
                Service.DataProvider.SaveCarte(carte);
                return RedirectToAction("Details", "Carte", new { area = "Identification", id = carte.id });
            //}
            //return View(model);
        }

        // GET: Identification/Carte/Create
        public ActionResult Create(string id)
        {
            Models.Entity.Identification entity = Service.DataProvider.FindIdentificationById(id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            if (!Service.DataProvider.GetLastCarte(id).Expired)
            {
                TempData["ErrorMessage"] = "Une autre carte est en cours d'utilissation, elle sera archivée à l'enregistrement de celle-ci";
                //return RedirectToAction("Details", "Record", new { id = id });
            }
            CarteViewModel model = new CarteViewModel
            {
                entityId = Guid.NewGuid().ToString(),
                IdentificationId = entity.id,
                DateExpirationCarte = DateTime.Today.AddYears(1).ToString("dd/MM/yyyy"),
                title = "Carte [Renouveler]"
            };
            //Carte current = Service.DataProvider.GetLastCarte(id);
            //if (current != null)
            //{

            //    model.AdresseQuartier = current.adresse_quartier;
            //    model.AdresseAvenue = current.adresse_avenue;
            //    model.AdresseCommuneId = current.adresse_commune_id;
            //    model.AdresseNumero = current.adresse_numero;
            //    model.DateExpirationCarte = current.date_expiration.AddYears(1);
            //    model.DateExpirationPassePort = current.passeport_exp_date;
            //    model.DateExpirationVisa = current.visa_exp_date;
            //    model.NumeroPassePort = current.passeport_numero;
            //    model.NumeroVisa = current.visa_numero;
            //    model.TypePassport = current.type_passeport_id;
            //    model.TypeVisa = current.type_visa_id;
            //} 
            ViewBag.Requerant = entity;
            return View(model);
        }

        // POST: Identification/Carte/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CarteViewModel model)
        {
            Carte carte = Service.DataProvider.FindCartById(model.entityId);

            if (ModelState.IsValid)
            { 
                if (carte == null)
                {
                    carte = new Carte
                    {
                        id = model.entityId,
                        identification_id = model.IdentificationId,
                        numero = Service.DataProvider.GetNewCarteNumero()
                    };
                }
                carte.adresse_quartier = model.AdresseQuartier;
                carte.adresse_avenue = model.AdresseAvenue;
                carte.adresse_commune_id = model.AdresseCommuneId;
                carte.adresse_numero = model.AdresseNumero;
                carte.date_expiration = DateTime.ParseExact(model.DateExpirationCarte, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                carte.passeport_exp_date = DateTime.ParseExact(model.DateExpirationPassePort, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                carte.visa_exp_date = DateTime.ParseExact(model.DateExpirationVisa, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                carte.passeport_numero = model.NumeroPassePort;
                carte.visa_numero = model.NumeroVisa;
                carte.type_passeport_id = model.TypePassport;
                carte.type_visa_id = model.TypeVisa;

                Service.DataProvider.SaveCarte(carte); 
                return RedirectToAction("Details", "Carte", new { area="Identification", id = carte.id });
            }
            ViewBag.Requerant = carte.Identification;
            return View("Create", model);
        }

        // GET: Identification/Carte/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = Service.DataProvider.FindCartById(id);
            if (carte == null)
            {
                return HttpNotFound();
            }

            CarteViewModel carteViewModel = new CarteViewModel
            {
                entityId = carte.id,
                IdentificationId = carte.identification_id,
                AdresseQuartier = carte.adresse_quartier,
                AdresseAvenue = carte.adresse_avenue,
                AdresseCommuneId = carte.adresse_commune_id,
                AdresseNumero = carte.adresse_numero,
                DateExpirationCarte = carte.date_expiration.ToString("dd/MM/yyyy"),

                DateExpirationPassePort = carte.passeport_exp_date.ToString("dd/MM/yyyy"),
                DateExpirationVisa = carte.visa_exp_date.ToString("dd/MM/yyyy"),
                NumeroPassePort = carte.passeport_numero,
                NumeroVisa = carte.visa_numero,
                TypePassport = carte.type_passeport_id,
                TypeVisa = carte.type_visa_id,
                title = "Carte [Modifier]"
            };

            ViewBag.Requerant = carte.Identification;
            return View("Create", carteViewModel);
        }

    }
}
