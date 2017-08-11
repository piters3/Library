﻿using Library.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Library.Controllers {
    public class CatalogController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string category, string currentFilter, string searchString, int? page, int limit = 0, bool onlyAvailable = false) {
            List<Category> categories = db.Categories.ToList();
            ViewBag.cat = categories;
            if (searchString != null) {
                page = 1;
            } else {
                searchString = currentFilter;
            }
            var books = from b in db.Books
                        select b;

            if (onlyAvailable) {
                books = books.Where(x => x.Amount != 0);
            }


            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(category)) {
                books = books.Where(x => x.Category.Name == category);
            }
            if (!String.IsNullOrEmpty(searchString)) {
                books = books.Where(b => b.Title.Contains(searchString)
                || b.Author.Contains(searchString)
                || b.Print.Contains(searchString));
            }
            books = books.OrderByDescending(b => b.Category.Name);
            int pageSize;
            if (limit != 0) {
                pageSize = limit;
            } else {
                pageSize = 3;
            }
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult BookDetails(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null) {
                return HttpNotFound();
            }
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrow(int id) {
            int daysToBorrow = 14;
            Book book = db.Books.Find(id);

            if (book.Amount == 0) {
                TempData["message"] = string.Format("Brak dostępnych egzemplarzy danej książki!!!");
                return RedirectToAction("Borrow", "Catalog", id);
            }
            Lending lend = new Lending {
                BookId = id,
                BorrowDate = DateTime.Now,
                FinedAmount = 0,
                UserId = User.Identity.GetUserId(),
                DaysLeft = daysToBorrow
            };

            book.Amount--;
            db.Entry(book).State = EntityState.Modified;
            db.Lendings.Add(lend);
            db.SaveChanges();

            TempData["message"] = string.Format("Książka została wypożyczona!!");
            return RedirectToAction("Index", "Manage", id);
        }

        public ActionResult UserBorrows() {
            string userId = User.Identity.GetUserId();
            var borrows = db.Lendings.Where(x => x.UserId == userId);
            return View(borrows.ToList());
        }
    }
}