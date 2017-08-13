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
                Book evaluatedBook = db.Books.Find(rating.BookId);
                evaluatedBook.AverageRatig = evaluatedBook.Ratings.Average(x => x.Rate);
                db.Entry(evaluatedBook).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Ocena została dodana!");
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rating.UserId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", rating.BookId);
            ModelState.AddModelError("", "Zjebało się");
            return View(rating);
        }


        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null) {
                return HttpNotFound();
            }
            ViewBag.Users = new SelectList(db.Users, "Id", "Username", rating.UserId);
            ViewBag.Books = new SelectList(db.Books, "BookId", "Title", rating.BookId);
            return View(rating);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rating rating) {
            if (ModelState.IsValid) {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Ocena została zedytowana!");
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList(db.Users, "UserId", "Username", rating.UserId);
            ViewBag.Books = new SelectList(db.Books, "BookId", "Title", rating.BookId);
            return View(rating);
        }


        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null) {
                return HttpNotFound();
            }
            return View(rating);
        }


        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null) {
                return HttpNotFound();
            }
            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
            db.SaveChanges();
            TempData["message"] = string.Format("Ocena została usunięta!");
            return RedirectToAction("Index");
        }

    }


}