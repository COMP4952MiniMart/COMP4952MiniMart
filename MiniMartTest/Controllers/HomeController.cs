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
        List<Models.Post> postList = new List<Models.Post>();
        List<Models.Image> imageList = new List<Models.Image>();


        public ActionResult Index()
        {
            using (var db = new MiniMartDBContentEntities())
            {

                //all posts
                var posts = db.Posts;

                foreach (Post post in posts)
                {
                    postList.Add(post);
                }

                //all images
                var images = db.Images;

                foreach (Image image in images)
                {
                    imageList.Add(image);
                }
            }
            Session["postList"] = postList;
            Session["imageList"] = imageList;

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

        // for infinite scrolling
        public ActionResult GetData(int pageIndex, int pageSize)
        {
            System.Threading.Thread.Sleep(1000); // for test
            var db = new MiniMartDBContentEntities();
            var query = (from post in db.Posts
                         orderby post.postId ascending
                         select post)
                     .Skip(pageIndex * pageSize)
                     .Take(pageSize);
            try
            {
                return Json(query.ToList<Post>(), JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(new List<Post>(), JsonRequestBehavior.AllowGet); // return empty list
            }
        }
        

        public ActionResult ZoomUpPost(int postId)
        {
            Post post;
            using (var db = new MiniMartDBContentEntities())
            {
                post = db.Posts.First(m=>m.postId == postId);

            }
            
            Session["SelectedPost"] = post;

            return RedirectToAction("Index");
        }

    }
}