using BlogWebPage.Models.Entity;
using BlogWebPage.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWebPage.Controllers
{
    public class AdminMainController : Controller
    {
        // GET: AdminMain
        readonly GenericRepository<Posts> repo = new GenericRepository<Posts>();
        public ActionResult Index()
        {
            var values = repo.List();
            return View(values);
        }
        public ActionResult AddPost()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPost(Posts p)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string ImgFile = "~/Image/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(ImgFile));
                p.Image = "/Image/" + filename + extension;
            }
            p.Date = DateTime.Now;
            p.Status = true;
            repo.Add(p);
            return RedirectToAction("Index");
        }
        public ActionResult EditPost(int id)
        {
            var value = repo.Get(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult EditPost(Posts t)
        {
            var value = repo.Get(t.ID);
            if (Request.Files.Count > 0 && !string.IsNullOrEmpty(Request.Files[0].FileName)) //&& !string.IsNullOrEmpty(Request.Files[0].FileName this code checks whether new img has added or not.
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string ImgFile = "~/Image/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(ImgFile));
                value.Image = "/Image/" + filename + extension;
            }
            if (t.Date.HasValue)
            {
                value.Date = t.Date.Value;
            }
            value.Subtitle = t.Subtitle;
            value.Title = t.Title;
            value.Status = true;
            value.Description = t.Description;
            value.MainText = t.MainText;
            repo.Update(value);
            return RedirectToAction("Index");
        }
        public ActionResult PassivePost(int id)
        {
            var values = repo.Find(x => x.ID == id);
            if (values.Status == false)
            {
                values.Status = true;
            }
            else
            {
                values.Status = false;
            }
            repo.Update(values);
            return RedirectToAction("Index");
        }
        public ActionResult DeletePost(int id)
        {
            var values = repo.Find(x => x.ID == id);
            repo.Delete(values);
            return RedirectToAction("Index");
        }

    }
}