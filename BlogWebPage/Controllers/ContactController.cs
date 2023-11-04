using BlogWebPage.Models.Entity;
using BlogWebPage.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWebPage.Controllers
{
    public class ContactController : Controller
    {
        GenericRepository<Contact> repo = new GenericRepository<Contact>();
        // GET: Contact
        public ActionResult Index()
        {
            var values = repo.List();
            return View(values);
        }
        public ActionResult DeleteMsg(int id) 
        {
            var value = repo.Find(x => x.ID == id);
            repo.Delete(value);
            return RedirectToAction("Index");
        }
    }
}