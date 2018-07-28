using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Homework.Controllers
{
    public class HomeController : BaseController
    {
        客戶資料Repository repo;
        public HomeController()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM logData)
        {
            if (repo.CheckLogin(logData) || (logData.帳號 == "admin" && logData.密碼 == "admin"))
            {
                string Level = "0";
                if (logData.帳號 == "admin")
                    Level = "1";

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, logData.帳號, DateTime.Now, DateTime.Now.AddMinutes(30), true,
                    Level, FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index");
        }
    }
}