using BlogWebPage.Models.Entity;
using BlogWebPage.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWebPage.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        readonly DbBlogEntities db = new DbBlogEntities();
        readonly GenericRepository<Posts> repo = new GenericRepository<Posts>();
        readonly GenericRepository<Images> repoImage = new GenericRepository<Images>();
        // GET: Main
        public ActionResult Index(int page = 1, int pageSize = 9)
        {
            var values = db.Posts.Where(x=>x.Status == true).ToList().ToPagedList(page, pageSize);
            return View(values);
        }

        public ActionResult DetailedPost(int id)
        {
            var value = repo.Find(x => x.ID == id);
            return View(value);
        }
        public PartialViewResult Contact() 
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Contact(Contact t)
        {
            t.Date = DateTime.Now;
            db.Contact.Add(t);
            db.SaveChanges();

            ViewBag.SuccessMessage = "Mesajınız başarıyla gönderildi.";
            return PartialView();
        }
        public ActionResult About() 
        {
            var values = db.About.ToList();
            return View(values);
        }
        public PartialViewResult SocialMedia() 
        {
            var value = db.SocialMedia.ToList();
            return PartialView(value);
        }
        public ActionResult ImageIndex(int page = 1, int pageSize = 9)
        {
            var values = db.Images.ToList().ToPagedList(page, pageSize);
            return View(values);
        }
        public ActionResult DetailedImagePost(int id)
        {
            var value = repoImage.Find(x => x.ID == id);
            return View(value);
        }
    }
}