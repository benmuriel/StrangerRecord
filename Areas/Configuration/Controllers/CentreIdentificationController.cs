using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.Configuration.Controllers
{
    public class CentreIdentificationController : Controller
    {
        
        // GET: Configuration/CentreIdentification
        public ActionResult Index()
        {

            return View(Service.DataProvider.LoadCentreIdentification());
        }
    }
}