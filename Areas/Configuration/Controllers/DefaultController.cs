using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.Configuration.Controllers
{
    [Authorize(Roles = "Configurateur")]
    public class DefaultController : Controller
    {
        // GET: Configuration/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}