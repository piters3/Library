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
    public class LendingsAdminController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult All() {
            var lendings = db.Lendings.ToList();
            int daysToBorrow = 14;

            foreach (var item in lendings) {
                if (item.ReturnDate == null && (item.BorrowDate.AddDays(daysToBorrow) < DateTime.Now)) {
                    var daysAbove = DateTime.Now - item.BorrowDate.AddDays(daysToBorrow);
                    int amount = daysAbove.Days * 2;
                    var daysleft = item.BorrowDate.AddDays(daysToBorrow) - DateTime.Now;
                    item.FinedAmount = amount;
                    item.DaysLeft = daysleft.Days;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.Title = "Wypożyczenia";
            return View(lendings);
        }

        public ActionResult Active() {
            var active = db.Lendings.Where(x => x.ReturnDate == null);
            ViewBag.Title = "Aktualne wypożyczenia";
            return View("All", active.ToList());
        }

        public ActionResult Archive() {
            var archive = db.Lendings.Where(x => x.ReturnDate != null);
            ViewBag.Title = "Archiwalne wypożyczenia";
            return View("All", archive.ToList());
        }

        public ActionResult Create() {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lending lending) {
            if (ModelState.IsValid) {
                int daysToBorrow = 14;
                var daysleft = lending.BorrowDate.AddDays(daysToBorrow) - DateTime.Now;
                lending.DaysLeft = daysleft.Days;
                db.Lendings.Add(lending);
                db.SaveChanges();
                TempData["message"] = string.Format("Wypożyczono książkę!");
                return RedirectToAction("All");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", lending.UserId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", lending.BookId);
            ModelState.AddModelError("", "Zjebało się");
            return View(lending);
        }


        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lending lending = db.Lendings.Find(id);
            if (lending == null) {
                return HttpNotFound();
            }
            ViewBag.Users = new SelectList(db.Users, "Id", "Username", lending.UserId);
            ViewBag.Books = new SelectList(db.Books, "BookId", "Title", lending.BookId);
            return View(lending);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lending lending) {
            if (ModelState.IsValid) {
                db.Entry(lending).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Wypożyczenie zostało zedytowane!");
                return RedirectToAction("All");
            }
            ViewBag.Users = new SelectList(db.Users, "UserId", "Username", lending.UserId);
            ViewBag.Books = new SelectList(db.Books, "BookId", "Title", lending.BookId);
            return View(lending);
        }


        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lending lending = db.Lendings.Find(id);
            if (lending == null) {
                return HttpNotFound();
            }
            return View(lending);
        }


        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lending lending = db.Lendings.Find(id);
            if (lending == null) {
                return HttpNotFound();
            }
            return View(lending);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Lending lending = db.Lendings.Find(id);
            db.Lendings.Remove(lending);
            db.SaveChanges();
            TempData["message"] = string.Format("Wypożyczenie zostało usunięte!");
            return RedirectToAction("All");
        }


        public ActionResult Return(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lending lending = db.Lendings.Find(id);
            if (lending == null) {
                return HttpNotFound();
            }
            return View(lending);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int id) {
            Lending lending = db.Lendings.Find(id);
            lending.ReturnDate = DateTime.Now;
            lending.DaysLeft = 0;
            lending.Book.Amount++;
            db.Entry(lending).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = string.Format("Książka została oddana!");
            return RedirectToAction("All");

        }
    }
}