using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.ViewModel
{
    public class SejourViewModel
    {
        [Key]
        public string entityId { get; set; }
        public string title { get; set; }
        public string carteId { get; set; }
        public string IdentificationId { get; set; }

        [Display(Name = "Motif du sejour (*)")]
        [Required(ErrorMessage = "Le motif du sejour est obligatoire")]
        public string MotifSejour { get; set; }


        [Display(Name = "Pays de provenance (*)")]
        [Required(ErrorMessage = "Le pays de provenance est obligatoire")]
        public string ProvenancePays { get; set; }


        [Display(Name = "Ville de provenance (*)")]
        [Required(ErrorMessage = "La ville de provenance est obligatoire")]
        public string ProvenanceVille { get; set; }

        [Display(Name = "Adresse ")]
        public string ProvenanceAdresse { get; set; }

        [Display(Name = "Pays de destination (*)")]
        [Required(ErrorMessage = "Le pays de destination est obligatoire")]
        public string DestinationPays { get; set; }


        [Display(Name = "Ville de destination (*)")]
        [Required(ErrorMessage = "La ville de destination est obligatoire")]
        public string DestinationVille { get; set; }

        [Display(Name = "Adresse ")]
        public string DestinatiomAdresse { get; set; }


        [Display(Name = "A partir du (*)")]
        [Required(ErrorMessage = "La date de debut du sejour est obligatoire")]
        //[DataType(DataType.Date)]
        public string DateDebutSejour { get; set; }


        [Display(Name = "Jusqu'au ")]
        //[DataType(DataType.Date)]
        public string DateFinSejour { get; set; }

    }
}