using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.ViewModel
{
    public class EditIdentificationViewModel
    {
        public string entityId { get; set; } 
        public string currentCarteId { get; set; } 

        [Display(Name = "Nom (*)")]
        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string nom { get; set; }

        [Display(Name = "Poste Nom")]
        public string postenom { get; set; }

        [Display(Name = "Prénom (*)")]
        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string prenom { get; set; }

        [Display(Name = "Genre")]
        public string genre { get; set; }

        [Display(Name = "Date de naissance (*)")]
        [Required(ErrorMessage = "La date de naissance est obligatoire")]
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-9]|3[0-1])/(0[0-9]|1[0-2])/(19|20)\d{2}$", ErrorMessage = "Date non valide")]
        //[DataType(DataType.Date)]
        public string date_naissance { get; set; }

        [Display(Name = "Proféssion (*)")]
        [Required(ErrorMessage = "La proféssion est obligatoire")]
        public string profession { get; set; }

        [Required(ErrorMessage = "Pays d'origine est obligatoire ")]
        [Display(Name = "Le pays d'origine ou pays d'origine (*)")]
        public string paysOrigine { get; set; }


        [Display(Name = "Adresse mail")]
        public string ContactMail { get; set; }

        [RegularExpression(@"^\+243(8[0-5]|9[0179])\d{7}$", ErrorMessage = "Numéro de téléphone non valide")]
        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire ")]
        [Display(Name = "Numero de telephone nationale (avec +243)")]
        public string ContactTelephone { get; set; }


        [Display(Name = "Ville de résidence dans le pays d'origine (*)")]
        [Required(ErrorMessage = "La ville de résidence dans le pays d'origine est obligatoire")]
        public string villeOrigine { get; set; }

        [Display(Name = "Date d'entrée en RDC (*)")]
        [Required(ErrorMessage = "La date d'entrée en RDC est obligatoire")]
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-9]|3[0-1])/(0[0-9]|1[0-2])/(19|20)\d{2}$", ErrorMessage = "Date non valide")]
        //[DataType(DataType.Date)]
        public string dateEntreeRdc { get; set; }
    }
}