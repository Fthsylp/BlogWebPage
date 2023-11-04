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
    public class AboutController : Controller
    {
        readonly GenericRepository<About> repo = new GenericRepository<About>();
        // GET: About
        public ActionResult Index()
        {
            var values = repo.List();
            return View(values);
        }
        [HttpPost]
        public ActionResult Index(About t, int id)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                var value = repo.Get(id);

                if (Request.Files.Count > 0 && !string.IsNullOrEmpty(Request.Files[0].FileName)) //&& !string.IsNullOrEmpty(Request.Files[0].FileName this code checks whether new img has added or not.
                {
                    string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string ImgFile = "~/Image/" + filename + extension;
                    Request.Files[0].SaveAs(Server.MapPath(ImgFile));
                    value.Image = "/Image/" + filename + extension;
                }

                value.Description1 = t.Description1;
                value.Description2 = t.Description2;
                value.Description = t.Description;

                repo.Update(value);

                return RedirectToAction("Index");
            }
        }

    }
}