using Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers {
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller {
        public UsersAdminController() {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager) {
            UserManager = userManager;
            RoleManager = roleManager;
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

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager {
            get {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index() {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create() {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles) {
            if (ModelState.IsValid) {
                var user = new ApplicationUser { UserName = userViewModel.UserName, Email = userViewModel.Email, RegisterDate = DateTime.Now, Enabled = true };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded) {
                    if (selectedRoles != null) {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded) {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                } else {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                TempData["message"] = string.Format("Użytkownik {0} został poprawnie utworzony", user.UserName);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel() {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                City = user.City,
                PostalCode = user.PostalCode,
                ImageData = user.ImageData,
                ImageMimeType = user.ImageMimeType,
                Enabled= user.Enabled,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem() {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, UserName, Email, Name, Surname, Address, City, PostalCode, ImageData, ImageMimeType, Enabled")] EditUserViewModel editUser, HttpPostedFileBase image = null, params string[] selectedRole) {
            if (ModelState.IsValid) {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null) {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.Email = editUser.Email;
                user.Name = editUser.Name;
                user.Surname = editUser.Surname;
                user.Address = editUser.Address;
                user.City = editUser.City;
                user.PostalCode = editUser.PostalCode;
                user.Enabled = editUser.Enabled;

                if (image != null) {
                    user.ImageMimeType = image.ContentType;
                    user.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(user.ImageData, 0, image.ContentLength);
                }

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded) {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded) {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                TempData["message"] = string.Format("Dane użytkownika {0} zostały zmienione", user.UserName);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            if (ModelState.IsValid) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null) {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded) {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
