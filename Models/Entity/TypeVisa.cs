using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{
    [Table("type_visa")]
    public class TypeVisa
    {
        public TypeVisa()
        {
            Cartes = new HashSet<Carte>();
        }
        [Key]
        public int id { get; set; }
        public string designation { get; set; }

        public virtual ICollection<Carte> Cartes { get; set; }
        public override string ToString()
        {
            return this.designation;
        }
    }
}