using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using Library.Entities;
using Microsoft.AspNet.Identity;

namespace Library.Controllers {
    [Authorize]
    public class ContactController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Ask() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ask(AskViewModel askViewModel) {
            if (ModelState.IsValid) {

                var subject = new Subject {
                    Name = askViewModel.Name,
                    Messages = new List<Message>()
                };

                var message = new Message {
                    Content = askViewModel.Content,
                    UserId = User.Identity.GetUserId(),
                    Data = DateTime.Now
                };

                subject.Messages.Add(message);

                db.Subjects.Add(subject);
                db.SaveChanges();

                TempData["message"] = string.Format("Zadałeś pytanie!");
                return RedirectToAction("Questions", "Contact");
            };
            return View();
        }

        public ActionResult Questions() {
            var UserId = User.Identity.GetUserId();
            var userSubjects = db.Subjects.Where(s => s.Messages.Any(m => m.UserId == UserId));
            return View(userSubjects.ToList());
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
                return RedirectToAction("Conversation", "Contact", id);
            };

            ModelState.AddModelError("", "Coś się zjebało");
            return View();
        }

    }
}