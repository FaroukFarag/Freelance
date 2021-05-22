using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freelance.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Posts()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult PostsRequests()
        {
            return View();
        }
    }
}