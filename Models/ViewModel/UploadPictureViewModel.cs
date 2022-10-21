using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StrangerRecord.Models.ViewModel
{
    public class UploadPictureViewModel
    {
        public string Name { get; set; }
        [Required]
        public string carteId { get; set; }

        [Required] 
        public HttpPostedFileBase Picture { get; set; }
    }
}