using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Areas.RapportFrequentation.Controllers
{
    public class DefaultController : Controller
    {
        // GET: RapportFrequentation/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}