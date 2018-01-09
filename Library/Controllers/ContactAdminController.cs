using Library.Entities;
using Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers {
    [Authorize(Roles = "admin")]
    public class ContactAdminController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.Subjects.ToList());
        }


        public ActionResult Conversation(int id) {
            var model = new ConversationViewModel {
                Messages = db.Messages.Where(s => s.SubjectId == id).ToList()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Conversation(ConversationViewModel conversationViewModel, int id) {
            if (ModelState.IsValid) {
                var message = new Message {
                    SubjectId = id,
                    Content = conversationViewModel.ReplyViewModel.Content,
                    UserId = User.Identity.GetUserId(),
                    Data = DateTime.Now
                };

                db.Messages.Add(message);
                db.SaveChanges();

                TempData["message"] = string.Format("Odpowiedź została wysłana!!");
                return RedirectToAction("Conversation", "ContactAdmin", id);
            };

            ModelState.AddModelError("", "Coś się zjebało");
            return View();
        }

    }
}