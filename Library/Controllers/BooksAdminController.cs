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
    public class BooksAdminController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.Books.ToList());
        }

        public ActionResult Create() {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {
                db.Books.Add(book);
                if (image != null) {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                db.SaveChanges();
                TempData["message"] = string.Format("Książka została dodana!");
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", book.CategoryId);
            ModelState.AddModelError("", "Zjebało się");
            return View(book);
        }


        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null) {
                return HttpNotFound();
            }
            EditBookViewModel editBookModel = new EditBookViewModel(book, db.Categories);
            return View(editBookModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {
                if (image != null) {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Książka została zedytowana!");
                return RedirectToAction("Index");
            }
            return View(book);
        }



        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null) {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null) {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            TempData["message"] = string.Format("Książka została usunięta!");
            return RedirectToAction("Index");
        }

    }
}
