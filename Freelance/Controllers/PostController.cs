using AutoMapper;
using Freelance.App_Start;
using Freelance.Core;
using Freelance.Core.Models;
using Freelance.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
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
        private readonly IMapper _mapper;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfiguration.Mapper;
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
        public ActionResult Posts(int? page, string SearchByTitle = null, string SearchByDate = null, string SearchByClientName = null)
        {
            IEnumerable<Post> posts = new List<Post>();
            IEnumerable<Proposal> proposals = new List<Proposal>();

            if (User.IsInRole("Admin") || User.IsInRole("Freelancer"))
            {
                posts = _unitOfWork.Posts.ListPosts(SearchByTitle, SearchByDate, SearchByClientName);
            }

            else if(User.IsInRole("Client"))
            {
                posts = _unitOfWork.Posts.ClientPosts(User.Identity.GetUserId(), SearchByTitle, SearchByDate, SearchByClientName);
            }

            else
            {
                posts = _unitOfWork.Posts.ListPosts(SearchByTitle, SearchByDate, SearchByClientName);
            }

            var model = new PostsViewModel
            {
                Posts = posts.ToList().Select(_mapper.Map<Post, PostViewModel>).ToPagedList(page ?? 1, 10),
                Proposals = _unitOfWork.Proposals.AllPostsProposals().ToLookup(p => p.PostId)
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PostsRequests(int? page)
        {
            var model = _unitOfWork.Posts.PostsRequests().Select(_mapper.Map<Post, PostRequestViewModel>);
            return View(model.ToPagedList(page ?? 1, 10));
        }

        [Authorize(Roles = "Admin,Client")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client")]
        public ActionResult Create(PostFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var user = UserManager.Users.Single(u => u.Id == userId);
            var post = _mapper.Map <PostFormViewModel, Post>(model);

            _unitOfWork.Posts.Add(post);
            _unitOfWork.Complete();

            return RedirectToAction("Posts");
        }

        [Authorize(Roles = "Admin,Client")]
        public ActionResult Edit(int id)
        {
            var post = _unitOfWork.Posts.GetPost(id);

            if (post == null)
            {
                ModelState.AddModelError("", "Post not found");

                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(_mapper.Map<Post, PostFormViewModel>(post));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client")]
        public ActionResult Edit(PostFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var user = UserManager.Users.Single(u => u.Id == userId);
            var post = _mapper.Map<PostFormViewModel, Post>(model);

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
                Post = _mapper.Map<Post, PostViewModel>(post),
                Proposals = _unitOfWork.Proposals.PostsProposals().ToLookup(p => p.PostId)
            };

            return View(model);
        }
    }
}