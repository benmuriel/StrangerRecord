using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.RapportFrequentation
{
     
        public class RapportFrequentationAreaRegistration : AreaRegistration
        {
            public override string AreaName
            {
                get
                {
                    return "RapportFrequentation";
                }
            }

            public override void RegisterArea(AreaRegistrationContext context)
            {
                context.MapRoute(
                    "RapportFrequentation_default",
                    "RapportFrequentation/{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    
}