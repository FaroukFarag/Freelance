using Freelance.Core;
using Freelance.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Freelance.Controllers.Api
{
    [Authorize]
    public class PostController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Accept(PostDto dto)
        {
            _unitOfWork.Posts.Accept(dto.PostId);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Client")]
        public IHttpActionResult Delete(int id)
        {
            var post = _unitOfWork.Posts.GetPost(id);

            if(post == null)
            {
                return BadRequest("Post not found.");
            }

            _unitOfWork.Posts.Remove(post);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
