using Library.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers {
    [Authorize(Roles = "Admin")]
    public class RatingsAdminController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.Ratings.ToList());
        }


        public ActionResult Create() {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rating rating) {
            if (ModelState.IsValid) {
                db.Ratings.Add(rating);
                db.SaveChanges();
                TempData["message"] = string.Format("Ocena została dodana!");
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rating.UserId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", rating.BookId);
            ModelState.AddModelError("", "Zjebało się");
            return View(rating);
        }
    }
}