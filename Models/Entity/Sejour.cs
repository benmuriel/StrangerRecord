using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{
    [Table("sejour")]
    public class Sejour
    {
        public Sejour()
        {
            this.id = Guid.NewGuid().ToString();
            this.created_at = DateTime.Now; 
        }
        [Key]
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? archived_at { get; set; }
        public string motif { get; set; }
        public DateTime date_debut { get; set; }
        public DateTime? date_fin { get; set; }
        public string destination_ville { get; set; }
        public string destination_pays { get; set; }
        public string destination_adresse { get; set; }
        public string provenance_ville { get; set; }
        public string provenance_pays { get; set; }



        public string carte_id { get; set; }  
        public string encodeur_id { get; set; }
        public int centre_id { get; set; }


        public Carte Carte { get; set; } 
        public ApplicationUser Encodeur { get; set; }
        public CentreEnregistrement Centre { get; set; }


        public bool Expired
        {
            get
            {
                return this.date_fin.HasValue && this.date_fin.Value < DateTime.Now;
            }
        }

        public override string ToString()
        {
            return this.destination_pays + " - " + this.destination_ville;

        }
    }
}