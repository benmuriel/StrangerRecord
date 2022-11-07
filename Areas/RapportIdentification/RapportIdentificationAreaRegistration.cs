using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.RapportIdentification
{
     
        public class RapportIdentificationAreaRegistration : AreaRegistration
        {
            public override string AreaName
            {
                get
                {
                    return "RapportIdentification";
                }
            }

            public override void RegisterArea(AreaRegistrationContext context)
            {
                context.MapRoute(
                    "RapportIdentification_default",
                    "RapportIdentification/{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    
}