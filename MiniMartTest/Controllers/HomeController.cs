using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniMartTest.Models;
using System.IO;

namespace MiniMartTest.Controllers
{
    public class HomeController : Controller
    {
        List<User> userList = new List<Models.User>();

        public ActionResult Index()
        {
            using (var db = new MiniMartDBContentEntities())
            {
                var users = db.Users;

                foreach (User user in users)
                {
                    userList.Add(user);
                }
            }
            ViewData["userList"] = userList;

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

        public ActionResult Post()
        {
            ViewBag.Message = "Your Post page.";

            return View();
        }

        public ActionResult Saved()
        {
            ViewBag.Message = "Your Saved Items page.";

            return View();
        }
    }
}