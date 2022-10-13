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
        private static ApplicationUser CurrentUser
        {
            get
            {
                return HttpContext.Current.GetOwinContext()
                                   .GetUserManager<ApplicationUserManager>()
                                   .FindById(HttpContext.Current.User.Identity.GetUserId());
            }
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

        public static Sejour GetLastSejour(string identificationId)
        {
            db = new IdentityContext();
            return db.Sejours.Include("Encodeur").Include("Centre").Include("Carte")
                .FirstOrDefault(e => e.archived_at == null && e.Carte.identification_id == identificationId);
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