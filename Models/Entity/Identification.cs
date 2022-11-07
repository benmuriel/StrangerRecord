using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{

    [Table("identification")]
    public class Identification
    {
        public Identification()
        {
            Cartes = new HashSet<Carte>(); 
        }
        [Key]
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public string nom { get; set; }
        public string postenom { get; set; }
        public string prenom { get; set; }
        public string genre { get; set; }
        public DateTime date_naissance { get; set; }
        public string profession { get; set; }
        public string pays_origine { get; set; }
        public string ville_pays_origine { get; set; }
        public DateTime date_entree_rdc { get; set; }

        public string contact_mail { get; set; }
        public string contact_telephone { get; set; }


        /// refrence
        public int centre_id { get; set; }
        public string encodeur_id { get; set; }

        public virtual CentreEnregistrement Centre { get; set; }
        public virtual ApplicationUser Encodeur { get; set; } 
        public virtual ICollection<Carte> Cartes { get; set; }

        public Carte CurrentCarte
        {
            get
            {
                return this.Cartes.FirstOrDefault(e => !e.Archived);
            }
        }
        public override string ToString()
        {
            return this.nom + " " + this.postenom + " " + this.prenom;
        }
    }
}