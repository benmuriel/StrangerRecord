using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{

    [Table("carte")]
    public class Carte
    {
        public Carte()
        {
            this.id = Guid.NewGuid().ToString();
            this.created_at = DateTime.Now;
            Sejours = new HashSet<Sejour>();
        }
        [Key]
        public string id { get; set; }
        public string numero { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? archived_at { get; set; }
      
        public DateTime date_delivrance { get; set; }
        public DateTime date_expiration { get; set; }
     
        public string adresse_quartier { get; set; }
        public string adresse_avenue { get; set; }
        public string adresse_numero { get; set; }

        public string visa_numero { get; set; } 
        public DateTime visa_exp_date { get; set; }
        public string passeport_numero { get; set; }
        public DateTime passeport_exp_date { get; set; }




        public int adresse_commune_id { get; set; }
        public string identification_id { get; set; }
        public string encodeur_id { get; set; }
        public int centre_id { get; set; }
        public int type_passeport_id { get; set; }
        public int type_visa_id { get; set; }


        public Commune Commune { get; set; }
        public Identification Identification { get; set; }
        public ApplicationUser Encodeur { get; set; }
        public CentreEnregistrement Centre { get; set; }
        public TypeVisa TypeVisa { get; set; }
        public TypePasseport TypePasseport { get; set; }



        public virtual ICollection<Sejour> Sejours { get; set; }

        public bool Archived
        {
            get
            {
                return  this.archived_at < DateTime.Now;
            }
        } 
        public bool Expired
        {
            get
            {
                return  this.date_expiration < DateTime.Now;
            }
        } 
        public bool PassePortExpired
        {
            get
            {
                return this.passeport_exp_date < DateTime.Now;
            }
        }
        public bool VisaExpired
        {
            get
            {
                return this.visa_exp_date  < DateTime.Now;
            }
        }

        public string FullAdresse
        {
            get
            {
                return this.adresse_avenue + " No " + this.adresse_numero + " / " + this.adresse_quartier + " / " + this.Commune.ToString();
            }
        }
    }
}