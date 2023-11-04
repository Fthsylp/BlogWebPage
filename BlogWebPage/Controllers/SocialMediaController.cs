using BlogWebPage.Models.Entity;
using BlogWebPage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWebPage.Controllers
{
    public class SocialMediaController : Controller
    {
        readonly GenericRepository<SocialMedia> repo = new GenericRepository<SocialMedia>();
        // GET: SocialMedia
        public ActionResult Index()
        {
            var values = repo.List();
            return View(values);
        }
        public ActionResult AddSocial()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSocial(SocialMedia p)
        {
            if (!ModelState.IsValid) 
            {
                return View("Index");
            }
            if(p.Name == "Instagram")
            {
                p.Sign = "ti-instagram mr-2";
            }
            else if (p.Name == "Linkedin")
            {
                p.Sign = "ti-linkedin mr-2";
            }
            else if (p.Name == "Facebook")
            {
                p.Sign = "ti-facebook mr-2";
            }
            else if (p.Name == "Twitter")
            {
                p.Sign = "ti-twitter mr-2";
            }
            else if (p.Name == "GitHub")
            {
                p.Sign = "ti-github mr-2";
            }
            else
            {
                p.Sign = "fab fa-question";
            }
            repo.Add(p);
            return RedirectToAction("Index");
        }
        public ActionResult EditSocial(int id) 
        {
            var value = repo.Get(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult EditSocial(SocialMedia t) 
        {
            var value = repo.Find(x=>x.ID == t.ID);
            value.Name = t.Name;
            value.Link = t.Link;
            if (value.Name == "Instagram")
            {
                value.Sign = "ti-instagram mr-2";
            }
            else if (value.Name == "Linkedin")
            {
                value.Sign = "ti-linkedin mr-2";
            }
            else if (value.Name == "Facebook")
            {
                value.Sign = "ti-facebook mr-2";
            }
            else if (value.Name == "Twitter")
            {
                value.Sign = "ti-twitter mr-2";
            }
            else if (value.Name == "GitHub")
            {
                value.Sign = "ti-github mr-2";
            }
            else
            {
                value.Sign = "fab fa-question";
            }
            repo.Update(value);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSocial(int id) 
        {
            var value = repo.Get(id);
            repo.Delete(value);
            return RedirectToAction("Index");
        }
    }
}