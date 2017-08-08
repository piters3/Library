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
    public class BookCopiesAdminController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.BookCopies.ToList());
        }

        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null) {
                return HttpNotFound();
            }
            return View(bookCopy);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookCopyId, BookId, Book, Amount")] BookCopy bookCopy) {
            if (ModelState.IsValid) {
                db.Entry(bookCopy).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Egzemplarz książki został zedytowany!");
                return RedirectToAction("Index");
            }
            return View(bookCopy);
        }



        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null) {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null) {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            BookCopy bookCopy = db.BookCopies.Find(id);
            db.BookCopies.Remove(bookCopy);
            db.SaveChanges();
            TempData["message"] = string.Format("Egzemplarz Książki został usunięty!");
            return RedirectToAction("Index");
        }

    }
}
