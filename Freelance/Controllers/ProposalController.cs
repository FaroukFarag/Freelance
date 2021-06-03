using Freelance.Core;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freelance.Controllers
{
    public class ProposalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProposalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Proposals(int? page)
        {
            var model = _unitOfWork.Proposals.PostsProposals().ToPagedList(page ?? 1, 1);

            return View(model);
        }
    }
}