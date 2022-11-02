using StrangerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StrangerRecord.Areas.Configuration.Controllers
{
    [Authorize(Roles = "Configurateur")]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }



        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult HasRole(string userid, string role)
        {
            bool check = UserManager.IsInRole(userid, role);
            ViewBag.Role = role;
            ViewBag.userid = userid;
            return View(check);
        }

        public ActionResult AjouterRole(string userid, string role)
        {
            UserManager.AddToRole(userid, role);
            return RedirectToAction("Details", "User", new { id = userid, area = "Configuration" });
        }
        public ActionResult RetirerRole(string userid, string role)
        {
            UserManager.RemoveFromRole(userid, role);
            return RedirectToAction("Details", "User", new { id = userid, area = "Configuration" });
        }
        public ActionResult LockAccount(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user != null)
            {
                if (user.LockoutEndDateUtc == null)
                    user.LockoutEndDateUtc = DateTime.Now.AddYears(100);
                else user.LockoutEndDateUtc = null;
                UserManager.Update(user);
            }
            return RedirectToAction("Details", "User", new { id = id, area = "Configuration" });
        }
        // GET: Configuration/User

        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        public ActionResult Details(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "User");
            }

            return View(user);
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "User");
            }
            return View(new EditUserViewModel { centreId = user.centreId, FullName = user.FullName, UserName = user.UserName, Id = user.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(model.Id);

                user.UserName = model.UserName;
                user.FullName = model.FullName;
                user.centreId = model.centreId;
                UserManager.Update(user);
                return RedirectToAction("Details", "User", new { id = user.Id, area = "Configuration" });

            }
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName, centreId = model.centreId, FullName = model.FullName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Pour plus d'informations sur l'activation de la confirmation de compte et de la réinitialisation de mot de passe, visitez https://go.microsoft.com/fwlink/?LinkID=320771
                    // Envoyer un message électronique avec ce lien
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.Se/ndEmailAsync(user.Id, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>");

                    return RedirectToAction("Details", "User", new { id = user.Id, area = "Configuration" });
                }
                AddErrors(result);
            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(model);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}