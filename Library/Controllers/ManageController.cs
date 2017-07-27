using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers {
    [Authorize]
    public class ManageController : Controller {
        public ManageController() {
        }

        public ManageController(ApplicationUserManager userManager) {
            UserManager = userManager;
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
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message) {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Twoje hasło zostało zmienione!"
                : message == ManageMessageId.Error ? "Wystąpił błąd"
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return View(user);
        }

        //
        // GET: /Manage/EditAccount
        public async Task<ActionResult> EditAccount() {

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return View(new EditAccountViewModel() {
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                City = user.City,
                PostalCode = user.PostalCode,
                ImageData=user.ImageData,
                ImageMimeType=user.ImageMimeType      
            });
        }

        //
        // POST: /Manage/EditAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAccount([Bind(Include = "UserName, Name, Surname, Address, City, PostalCode, ImageData, ImageMimeType")] EditAccountViewModel editAccount, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {             
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                user.UserName = editAccount.UserName;
                user.Name = editAccount.Name;
                user.Surname = editAccount.Surname;
                user.Address = editAccount.Address;
                user.City = editAccount.City;
                user.PostalCode = editAccount.PostalCode;

                if (image != null) {
                    user.ImageMimeType = image.ContentType;
                    user.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(user.ImageData, 0, image.ContentLength);
                }

                IdentityResult result = await UserManager.UpdateAsync(user);

                TempData["message"] = string.Format("Dane konta zostały zmienione");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword() {
            return View();
        }

        //
        // POST: /Account/Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded) {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null) {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent) {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}