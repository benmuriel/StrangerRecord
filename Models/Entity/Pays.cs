using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.Entity
{
    [Table("pays")]
    public class Pays
    {
        public string id { get; set; }
    }
}