using Freelance.Core;
using Freelance.Core.Models;
using Freelance.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            IEnumerable<Post> posts = new List<Post>();

            if (User.IsInRole("Admin") || User.IsInRole("Freelancer"))
            {
                posts = _unitOfWork.Posts.ListPosts();
            }

            else if(User.IsInRole("Client"))
            {
                posts = _unitOfWork.Posts.ClientPosts(User.Identity.GetUserId());
            }

            else
            {
                posts = _unitOfWork.Posts.ListPosts();
            }

            var model = new PostsViewModel
            {
                Posts = posts,
                Proposals = _unitOfWork.Proposals.PostsProposals().ToLookup(p => p.FreelancerId)
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostFormViewModel model)
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

        public ActionResult Edit(int id)
        {
            var post = _unitOfWork.Posts.GetPost(id);

            if (post == null)
            {
                ModelState.AddModelError("", "Post not found");

                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var model = new PostFormViewModel
            {
                Id = post.Id,
                JobType = post.JobType,
                JobBudget = post.JobBudget,
                JobDescription = post.JobDescription,
                IsAccepted = post.IsAccepted
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var user = UserManager.Users.Single(u => u.Id == userId);
            var post = new Post
            {
                Id = model.Id,
                ClientName = user.FirstName + " " + user.LastName,
                JobType = model.JobType,
                JobBudget = model.JobBudget,
                CreationDate = DateTime.Now,
                JobDescription = model.JobDescription,
                IsAccepted = model.IsAccepted,
                ClientId = userId

            };

            _unitOfWork.Posts.Update(post);
            _unitOfWork.Complete();

            return RedirectToAction("Posts");
        }

        public ActionResult Details(int id)
        {
            var post = _unitOfWork.Posts.GetPost(id);

            if (post == null)
            {
                ModelState.AddModelError("", "Post not found");

                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var model = new PostDetailsViewModel
            {
                Id = post.Id,
                ClientName = post.ClientName,
                JobType = post.JobType,
                JobBudget = post.JobBudget,
                CreationDate = post.CreationDate,
                JobDescription = post.JobDescription,
                Client = post.Client,
                Proposals = _unitOfWork.Proposals.PostsProposals().ToLookup(p => p.FreelancerId)
            };

            return View(model);
        }

        public ActionResult PostsRequests()
        {
            return View(_unitOfWork.Posts.PostsRequests());
        }
    }
}