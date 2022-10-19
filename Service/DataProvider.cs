using Microsoft.AspNet.Identity;
using StrangerRecord.Models;
using StrangerRecord.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace StrangerRecord.Service
{


    public static class DataProvider
    {
        private static IdentityContext db = new IdentityContext();

        public static List<SelectListItem> GenderFormChoiceList
        {
            get
            {
                return new List<SelectListItem> {
                    new SelectListItem { Value = "Homme", Text = "Homme" },
                    new SelectListItem { Value = "Femme", Text = "Femme" }
                };
            }
        }

        public static Identification FindIdentificationById(string id)
        {
            return db.Identifications.Find(id);
        }

        internal static List<Carte> LoadCarteBy(string type, string q)
        {
            q = q.Trim().ToLower();
            switch (type)
            {
                case "numpassport":
                    return db.Cartes.Include("Identification").Where(e => e.passeport_numero.Trim().ToLower() == q).OrderByDescending(e => e.created_at).Take(50).ToList();
                case "nom":
                    return db.Cartes.Include("Identification")
                        .Where(e => e.Identification.nom.Contains(q) || e.Identification.postenom.Trim().ToLower().Contains(q) || e.Identification.prenom.Trim().ToLower().Contains(q))
                        .OrderByDescending(e => e.created_at).Take(50).ToList();
                case "numcarte":
                    return db.Cartes.Include("Identification").Where(e => e.numero.Trim().ToLower() == q).OrderByDescending(e=>e.created_at).Take(50).ToList();
            }
            return null;
        }

        internal static Sejour FindSejourById(string id)
        {
            return db.Sejours.Include("Carte").FirstOrDefault(d => d.id == id);
        }

        private static ApplicationUser CurrentUser
        {
            get
            {
                return HttpContext.Current.GetOwinContext()
                                   .GetUserManager<ApplicationUserManager>()
                                   .FindById(HttpContext.Current.User.Identity.GetUserId());
            }
        }

        internal static void SaveCarte(Carte carte)
        {
            if (FindCartById(carte.id) == null)
            {

                // invalider l ancienne carte

                Carte ancienne = Service.DataProvider.GetLastCarte(carte.identification_id);
                if (ancienne != null)
                {
                    ancienne.archived_at = DateTime.Now; 
                }
                carte.date_delivrance = DateTime.Today;
                carte.encodeur_id = CurrentUser.Id;
                carte.centre_id = CurrentUser.centreId;
                db.Cartes.Add(carte);
            }
            db.SaveChanges();
        }

        public static List<SelectListItem> CommuneByVilleFormChoiceList
        {
            get
            {
                ApplicationUser user = CurrentUser;

                List<SelectListItem> listItems = new List<SelectListItem>();
                db.Communes.Include("ville")
                    .Where(e => e.ville_id == (db.CentreEnregistrements.FirstOrDefault(r => r.id == user.centreId)).ville_id)
                    .ToList().ForEach(item => listItems.Add(new SelectListItem { Value = item.id.ToString(), Text = item.ToString() }));
                return listItems;
            }
        }

        internal static void SaveSejour(Sejour sejour)
        {
            if (FindSejourById(sejour.id) == null)
            { 
                // invalider l ancienne carte

                Sejour ancienne = Service.DataProvider.GetLastSejour(sejour.carte_id);
                if (ancienne != null)
                {
                    ancienne.archived_at = DateTime.Now;
                } 
                sejour.encodeur_id = CurrentUser.Id;
                sejour.centre_id = CurrentUser.centreId;
                db.Sejours.Add(sejour);
            }
            db.SaveChanges();
        }

        public static Carte FindCartById(string id)
        {
            return db.Cartes.Include("Identification").Include("Commune").FirstOrDefault(d=>d.id == id);
        }

        public static List<SelectListItem> TypeVisaFormChoiceList
        {
            get
            {

                List<SelectListItem> listItems = new List<SelectListItem>();
                db.TypeVisas
                    .ToList().ForEach(item => listItems.Add(new SelectListItem { Value = item.id.ToString(), Text = item.ToString() }));
                return listItems;
            }
        }
        public static List<SelectListItem> TypePassportFormChoiceList
        {
            get
            {

                List<SelectListItem> listItems = new List<SelectListItem>();
                db.TypePasseports
                    .ToList().ForEach(item => listItems.Add(new SelectListItem { Value = item.id.ToString(), Text = item.ToString() }));
                return listItems;
            }
        }


        public static Carte GetLastCarte(string identificationId)
        {
            db = new IdentityContext();
            return db.Cartes.Include("Encodeur").Include("Centre").Include("Commune").Include("TypeVisa").Include("TypePasseport")
                .FirstOrDefault(e => e.archived_at == null && e.identification_id == identificationId);
        }



        public static Sejour GetLastSejour(string carteid)
        {
            db = new IdentityContext();
            return db.Sejours.Include("Encodeur").Include("Centre").Include("Carte").OrderByDescending(e=>e.created_at)
                .FirstOrDefault(e =>  e.carte_id == carteid);
        }

        public static string GetNewCarteNumero()
        {
            return RandomString("char", 2) + RandomString("number", 6) + RandomString("char", 2); // 10

        }
        public static string RandomString(string type, int length)
        {
            Random ran = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string nums = "0123456789";
            switch (type)
            {
                case "char":
                    return new string(Enumerable.Repeat(chars, length).Select(r => r[ran.Next(r.Length)]).ToArray());
                case "number":
                    return new string(Enumerable.Repeat(nums, length).Select(r => r[ran.Next(r.Length)]).ToArray());
            }
            return new string(Enumerable.Repeat(chars + nums, length).Select(r => r[ran.Next(r.Length)]).ToArray());
        }

        public static CentreEnregistrement FindCentreById(int centreId)
        {
            return db.CentreEnregistrements.Include("ville").FirstOrDefault(e => e.id == centreId);
        }

        internal static void SaveIdentification(Identification identification)
        {
            if (FindIdentificationById(identification.id) == null)
                db.Identifications.Add(identification);
            db.SaveChanges();
        }

        //public static string UniqueRamdonString(string source, string serie)
        //{
        //    source = source.Trim().Replace(' ', 'W').ToUpper();
        //    return serie +                       //2
        //       RandomString("number", 3) +  //6
        //       source.Substring(0, 1) +             //1
        //       RandomString("char", 2) +    //4
        //       source.Substring(source.Length - 1, 1) +   //1
        //       RandomString("number", 3) +  //6
        //       RandomString("char", 2);     //2
        //}
    }
}