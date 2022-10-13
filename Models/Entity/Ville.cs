using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{
    [Table("ville")]

    public class Ville
    {
        [Key]
        public int id { get; set; }
        public string designation { get; set; }

        public virtual ICollection<Commune> Communes { get; set; }
        public virtual ICollection<CentreEnregistrement> Centres { get; set; }
    }
}