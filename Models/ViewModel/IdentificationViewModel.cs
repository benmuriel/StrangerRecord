using System;
using System.ComponentModel.DataAnnotations;

namespace StrangerRecord.Models.ViewModel
{
    public class IdentificationViewModel
    {

        public string id { get; set; } = Guid.NewGuid().ToString();

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
        [DataType(DataType.Date)]
        public DateTime date_naissance { get; set; }

        [Display(Name = "Proféssion (*)")]
        [Required(ErrorMessage = "La proféssion est obligatoire")]
        public string profession { get; set; }

        [Required(ErrorMessage = "Pays d'origine est obligatoire ")]
        [Display(Name = "Le pays d'origine ou pays d'origine (*)")]
        public string paysOrigine { get; set; }


        [Display(Name = "Adresse mail")]
        public string ContactMail { get; set; }

        [Display(Name = "Numero de telephone")]
        public string ContactTelephone { get; set; }


        [Display(Name = "Ville de résidence dans le pays d'origine (*)")]
        [Required(ErrorMessage = "La ville de résidence dans le pays d'origine est obligatoire")]
        public string villeOrigine { get; set; }

        [Display(Name = "Date d'entrée en RDC (*)")]
        [Required(ErrorMessage = "La date d'entrée en RDC est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime dateEntreeRdc { get; set; }



        // --------- visa --------------------------

        [Display(Name = "Type  de Visa (*)")]
        [Required(ErrorMessage = "Le type de visa est obligatoire")]
        public int TypeVisa { get; set; }


        [Display(Name = "Type  de passeport (*)")]
        [Required(ErrorMessage = "Le type de passport est obligatoire")]
        public int TypePassport { get; set; }

        [Display(Name = "Numéro (*)")]
        [Required(ErrorMessage = "Le numéro du Visa est obligatoire")]

        public string NumeroVisa { get; set; }

        [Display(Name = "Date d'expiration (*)")]
        [Required(ErrorMessage = "La date d'expiration du visa est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateExpirationVisa { get; set; }

        [Display(Name = "Numéro (*)")]
        [Required(ErrorMessage = "Le numéro du passeport est obligatoire")]
        public string NumeroPassePort { get; set; }

        [Display(Name = "Date d'expiration (*)")]
        [Required(ErrorMessage = "La date d'expiration du passeport est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateExpirationPassePort { get; set; }


        // --------------- sejour ---------------------



        [Display(Name = "Motif du sejour (*)")]
        [Required(ErrorMessage = "Le motif du sejour est obligatoire")]
        public string MotifSejour { get; set; }


        //[Display(Name = "Pays de provenance (*)")]
        //[Required(ErrorMessage = "Le pays de provenance est obligatoire")]
        //public string ProvenancePays { get; set; }


        //[Display(Name = "Ville de provenance (*)")]
        //[Required(ErrorMessage = "La ville de provenance est obligatoire")]
        //public string ProvenanceVille { get; set; }

        //[Display(Name = "Adresse ")]
        //public string ProvenanceAdresse { get; set; }


        //[Display(Name = "Durée du sejour (*)")]
        //[Required(ErrorMessage = "La durée du sejour est obligatoire")]
        //public int SejourDuree { get; set; }


        [Display(Name = "A partir du (*)")]
        [Required(ErrorMessage = "La date de debut du sejour est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateDebutSejour { get; set; }


        [Display(Name = "Jusqu'au ")]
        [DataType(DataType.Date)]
        public DateTime? DateFinSejour { get; set; }


        //--------------- carte ---------------------
        [Display(Name = "Date d'expiration (*)")]
        [Required(ErrorMessage = "La date d'expiration de la carte est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateExpirationCarte { get; set; }

        [Display(Name = "Ville / Commune  (*)")]
        [Required(ErrorMessage = "La commune de résidence est obligatoire")]
        public int AdresseCommuneId { get; set; }


        [Display(Name = "L'avenue (*)")]
        [Required(ErrorMessage = "L'avenue de résidence est obligatoire")]
        public string AdresseAvenue { get; set; }


        [Display(Name = "Quartier (*)")]
        [Required(ErrorMessage = "Le quartier de résidence est obligatoire")]
        public string AdresseQuartier { get; set; }

        [Display(Name = "Au numéro")]
        [Required(ErrorMessage = "Le numero de résidence est obligatoire")]

        public string AdresseNumero { get; set; }
    }
}