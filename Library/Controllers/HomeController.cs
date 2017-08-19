using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Threading.Tasks;
using Library.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;

namespace Library.Controllers {
    public class HomeController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        public ActionResult Index() {
            return View();
        }

        public async Task<FileContentResult> GetImage() {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null) {
                return File(user.ImageData, user.ImageMimeType);
            } else {
                return null;
            }
        }

        public FilePathResult GetDefaultImage() {
            string path = "~\\Content\\Img\\default.jpg";
            string type = "image/jpeg";
            return File(path, type);
        }


        public async Task<FileContentResult> GetImageById(string id) {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null) {
                return File(user.ImageData, user.ImageMimeType);
            } else {
                return null;
            }
        }

        public FileContentResult GetCoverImageById(int id) {
            var book = db.Books.Find(id);
            if (book != null) {
                return File(book.ImageData, book.ImageMimeType);
            } else {
                return null;
            }
        }

        public FilePathResult GetDefaultCoverImage() {
            string path = "~\\Content\\Img\\defaultCover.jpg";
            string type = "image/jpeg";
            return File(path, type);
        }

        public FilePathResult GetImageByRating(int id) {
            string path = "~\\Content\\Img\\" + id + ".png";
            string type = "image/png";
            return File(path, type);
        }

        public ActionResult Chat() {
            return View();
        }
    }
}
