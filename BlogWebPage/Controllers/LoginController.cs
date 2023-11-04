using BlogWebPage.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogWebPage.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        DbBlogEntities db = new DbBlogEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login p)
        {
            var user = db.Login.FirstOrDefault(x => x.Username == p.Username && x.Password == p.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                Session["Username"] = user.Username.ToString();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { error = "Şifre yada Kullanıcı adı yanlış." });
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
