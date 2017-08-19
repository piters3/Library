using Library.Entities;
using Library.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Library.Controllers {
    public class AnnouncementsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(db.Announcements.ToList());
        }

        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement an = db.Announcements.Find(id);
            if (an == null) {
                return HttpNotFound();
            }
            return View(an);

        }
    }
}