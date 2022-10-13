using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{
    [Table("commune")]
    public class Commune
    {
        public Commune()
        {
            Cartes = new HashSet<Carte>();
        }
        [Key]
        public int id { get; set; }
        public string designation { get; set; }

        public int ville_id { get; set; }
        public virtual Ville ville { get; set; }


        public virtual ICollection<Carte> Cartes { get; set; }

        public override string ToString()
        {
            return this.ville.designation+" / " + this.designation;

        }
    }
}