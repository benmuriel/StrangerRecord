using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{
    [Table("centre")]
    public class CentreEnregistrement
    {
        public int id { get; set; }
        public string designation { get; set; }
        public string chef_centre { get; set; }

        public int ville_id { get; set; }

        public Ville ville { get; set; }

        public virtual ICollection<Carte> Cartes { get; set; }

        public virtual ICollection<Identification> Identifications { get; set; }
        public virtual ICollection<Sejour> Sejours { get; set; }

        public override string ToString()
        {
            return this.designation;
        }
    }


}