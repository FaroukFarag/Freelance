using Freelance.Core;
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

        public ActionResult Proposals()
        {
            var model = _unitOfWork.Proposals.PostsProposals();
            return View(model);
        }
    }
}