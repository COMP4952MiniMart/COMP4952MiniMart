using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniMartTest.Models;

namespace MiniMartTest.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Post()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Post(PostViewModel model)
        {
            using (var db = new MiniMartDBContentEntities())
            {
                string name = model.ItemName;
                Post post = new Post();
                post.name = model.ItemName;
                if (model.SelectedConditionId == 0)
                {
                    post.condition = "new";
                }
                else
                {
                    post.condition = "used";
                }
                post.description = model.ItemDiscription;


                db.Posts.Add(post);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult addImage(Image image)
        {


            return View();
        }
    }

}

    