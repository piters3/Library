using Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers {
    [Authorize(Roles = "admin")]
    public class PanelAdminController : Controller {
        public PanelAdminController() {
        }

        public PanelAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        //
        // GET: /PanelAdmin/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /PanelAdmin/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var user = UserManager.FindByName(model.UserName);
            if (user == null) {
                ModelState.AddModelError("", "Podany użytkownik nie istnieje");
                return View(model);
            } else {
                if (!UserManager.IsEmailConfirmed(user.Id)) {
                    ModelState.AddModelError("", "Podane konto nie zostało aktywowane");
                    return View(model);
                } else if (!user.Enabled) {
                    ModelState.AddModelError("", "To konto zostało zablokowane");
                    return View(model);
                } else if (!UserManager.IsInRole(user.Id, "Admin")) {
                    ModelState.AddModelError("", "Nie masz wystarczających uprawnień do tego zasobu");
                    return View(model);
                }
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            switch (result) {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                    ModelState.AddModelError("", "Błąd logownia");
                    return View(model);
                default:
                    ModelState.AddModelError("", "Błędne dane logowania");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "PanelAdmin");
        }

        public ActionResult Index() {
            return View();
        }
    }
}