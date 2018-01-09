using Library.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers {
    [Authorize(Roles = "admin")]
    public class AnnouncementsAdminController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.Announcements.ToList());
        }


        public ActionResult Create() {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Announcement an) {
            if (ModelState.IsValid) {
                db.Announcements.Add(an);
                db.SaveChanges();
                TempData["message"] = string.Format("Ogłoszenie zostało dodane!");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Zjebało się");
            return View(an);
        }


        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement an = db.Announcements.Find(id);
            if (an == null) {
                return HttpNotFound();
            }
            return View(an);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Announcement an) {
            if (ModelState.IsValid) {
                db.Entry(an).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Ogłoszenie zostało zedytowane!");
                return RedirectToAction("Index");
            }
            return View(an);
        }
    }
}