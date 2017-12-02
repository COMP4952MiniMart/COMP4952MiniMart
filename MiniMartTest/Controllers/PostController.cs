using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniMartTest.Models;
using System.Threading.Tasks;

namespace MiniMartTest.Controllers
{
    public class PostController : Controller
    {
        // store images before a post is stored in DB
        public List<Image> postImages = new List<Image>();
        private MiniMartDBContentEntities db = new MiniMartDBContentEntities();

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
            int postId = db.Posts.Count();
            Post post = new Post();
            post.name = model.ItemName;
            post.userId = 0;
            post.User = db.Users.First(m => m.Id == 0);
            post.Users.Add(post.User);

            if (model.SelectedConditionId == 0)
                post.condition = "new";
            else
                post.condition = "used";

            post.description = model.ItemDiscription;
            post.postId = postId;

            //add a new post 
            db.Posts.Add(post);
            db.SaveChanges();

            var images = Session["postImages"] as List<Image>;
             
            //add a new image
            foreach(Image i in images)
            {
                i.Id = db.Images.Count();
                i.postId = postId;
                db.Images.Add(i);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index", "Home");
        }

        public async Task<Action> UploadFile()
        {
            bool isSavedSuccessfully = true;
            string fname = "";
           
            try
            {
                foreach (string filename in Request.Files)
                {
                    HttpPostedFileBase image = Request.Files[filename];
                    Image i = new Image();
                    i.imageBinary = new byte[image.ContentLength];
                    image.InputStream.Read(i.imageBinary, 0, image.ContentLength);

                    var postImages = Session["postImages"] as List<Image>;
                    if(postImages == null)
                    {
                        postImages = new List<Image>();
                    }

                    postImages.Add(i);
                    Session["postImages"] =postImages;
                }

                //Session["postImages"] = postImages;
            }
            catch (Exception)
            {
                isSavedSuccessfully = false;
            }

            /* 
          if(isSavedSuccessfully)
          {
              return Json(new { Message = fname });
          }
          else
          {
              return Json(new { Message = "error" });
          }
          */
            return null;
        }

    }

    

}

    