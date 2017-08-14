using Library.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers {
    public class OpinionsAdminController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.Opinions.ToList());
        }


        public ActionResult Create() {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Opinion opinion) {
            if (ModelState.IsValid) {
                db.Opinions.Add(opinion);
                db.SaveChanges();
                TempData["message"] = string.Format("Opinia została dodana!");
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", opinion.UserId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", opinion.BookId);
            ModelState.AddModelError("", "Zjebało się");
            return View(opinion);
        }
    }
}