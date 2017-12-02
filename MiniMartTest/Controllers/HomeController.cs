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
        private MiniMartDBContentEntities db = new MiniMartDBContentEntities();

        public ActionResult Index()
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

        public ActionResult SavePost(int postId)
        {
            ViewBag.Message = "Your Saved Items page.";
           
             //TODO: store the postId in a user-based list

            return View();
        }

        // for infinite scrolling
        public ActionResult GetData(int pageIndex, int pageSize)
        {
            System.Threading.Thread.Sleep(1000); // for test
            var query = db.Posts.ToList<Post>();
            List<JsonModel> jsonModels = new List<JsonModel>();
            foreach(Post p in query)
            {
                var m = new JsonModel();
                m.postName = p.name;
                m.postId = p.postId;

                foreach(Image i in db.Images.ToList())
                {
                    if (i.postId == p.postId)
                    {
                        var base64 = Convert.ToBase64String(i.imageBinary);
                        var imageSrc = string.Format("data:image/gif;base64,{0}", base64);
                        m.imageSrc = imageSrc;
                        break;
                    }
                }

                jsonModels.Add(m);
            }
            
            try
            {
                return Json(jsonModels, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(new List<Post>(), JsonRequestBehavior.AllowGet); // return empty list
            }
        }
        
        //collection detail info of selected post and pass data to the view
        public ActionResult ZoomUpPost(int postId)
        {
            //define the post by postId from item click at the main page
            Post post;
            
            post = db.Posts.First(m=>m.postId == postId);

            
            
            //setup data for popup view displaying more detail
            Session["SelectedPost"] = post;

            return RedirectToAction("Index");
        }

    }
}

public class JsonModel {
        public string postName { get; set; }
        public int postId { get; set; } 
        
        public string imageSrc { get; set; }
}
