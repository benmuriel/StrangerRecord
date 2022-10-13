using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.ViewModel
{
    public class CarteViewModel
    {
        [Key]
        public string numero { get; set; }

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


        // ---------------carte -------------------------
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