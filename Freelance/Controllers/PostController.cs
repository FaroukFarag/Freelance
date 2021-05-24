using Freelance.Core;
using Freelance.Core.Models;
using Freelance.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freelance.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Posts()
        {
            return View(_unitOfWork.Posts.ListPosts());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var user = UserManager.Users.Single(u => u.Id == userId);
            var post = new Post
            {
                ClientName = user.FirstName + " " + user.LastName,
                JobType = model.JobType,
                JobBudget = model.JobBudget,
                CreationDate = DateTime.Now,
                JobDescription = model.JobDescription,
                ClientId = userId

            };

            _unitOfWork.Posts.Add(post);
            _unitOfWork.Complete();

            return RedirectToAction("Posts");
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
            return View(_unitOfWork.Posts.PostsRequests());
        }

        public ActionResult Accept(int id)
        {
            _unitOfWork.Posts.Accept(id);
            _unitOfWork.Complete();

            return RedirectToAction("PostsRequests");
        }

        public ActionResult Apply(int id)
        {
            var userId = User.Identity.GetUserId();

            _unitOfWork.Proposals.Add(userId, id);
            _unitOfWork.Complete();

            return RedirectToAction("Posts");
        }

        public ActionResult Proposals()
        {
            return View(_unitOfWork.Proposals.PostsProposals());
        }
    }
}