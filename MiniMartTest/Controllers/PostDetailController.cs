using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniMartTest.Models;

namespace MiniMartTest.Controllers
{
    public class PostDetailController : Controller
    {
        List<Image> imageList = new List<Image>();
        private MiniMartDBContentEntities db = new MiniMartDBContentEntities();
        // GET: PostDetail
        public ActionResult Index(int postId)
        {
            //define the post by postId that we got from viewItemPage button
            var post = Session["SelectedPost"] as Post;
            var images = db.Images;

            foreach(Image i in images)
            {
                if(i.postId == post.postId)
                {
                    imageList.Add(i);
                }
            }

            //setup data needed for the View 
            ViewData["imagesForThePost"] = imageList;
               
            

            return View("PostDetail");
        }
    }
}