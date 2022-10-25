using StrangerRecord.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Controllers
{
    [Authorize(Roles = "Verificateur")]
    public class VerificationController : Controller
    {
        // GET: Recherche
        public ActionResult Index(string q, string type)
        {
            List<Carte> datas = new List<Carte>();
            if(!String.IsNullOrEmpty(q) && !String.IsNullOrEmpty(type))
            {
                datas = Service.DataProvider.LoadCarteBy(type, q);
            }
            ViewBag.q = q;
            ViewBag.type = type;
            return View(datas);
        }
    }
}