using StrangerRecord.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrangerRecord.Controllers
{
    public class VerificationController : Controller
    {
        // GET: Recherche
        public ActionResult Index(string q, string type)
        {
            List<Carte> datas = new List<Carte>();
            if(!String.IsNullOrEmpty(q) && !String.IsNullOrEmpty(type))
            {
                datas = Service.DataProvider.FindCarteBy(type, q);
            }
            ViewBag.q = q;
            ViewBag.type = type;
            return View(datas);
        }
    }
}