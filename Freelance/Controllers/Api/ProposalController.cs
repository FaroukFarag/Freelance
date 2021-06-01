using Freelance.Core;
using Freelance.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace Freelance.Controllers.Api
{
    [RoutePrefix("api/Proposal")]
    public class ProposalController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProposalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Apply")]
        public IHttpActionResult Apply([FromBody] ProposalDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Proposals.GetProposal(userId, dto.PostId);

            if(exists != null)
            {
                return BadRequest();
            }

            _unitOfWork.Proposals.Add(userId, dto.PostId);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        [Route("Accept")]
        public IHttpActionResult Accept([FromBody] ProposalDto dto)
        {
            var exists = _unitOfWork.Proposals.GetProposal(dto.FreelancerId, dto.PostId);

            if (exists == null)
            {
                return NotFound();
            }

            _unitOfWork.Proposals.Accept(dto.FreelancerId, dto.PostId);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] ProposalDto dto)
        {
            var exists = _unitOfWork.Proposals.GetProposal(dto.FreelancerId, dto.PostId);

            if (exists == null)
            {
                return NotFound();
            }

            _unitOfWork.Proposals.Remove(dto.FreelancerId, dto.PostId);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
