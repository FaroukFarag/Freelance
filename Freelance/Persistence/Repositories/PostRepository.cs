using Freelance.Core.IRepositories;
using Freelance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
        }

        public IEnumerable<Post> ListPosts()
        {
            return _context.Posts.Where(p => p.IsAccepted).ToList();
        }

        public IEnumerable<Post> PostsRequests()
        {
            return _context.Posts.Where(p => !p.IsAccepted).ToList();
        }

        public void Accept(int id)
        {
            var post = _context.Posts.Single(p => p.Id == id);

            post.IsAccepted = true;
        }
    }
}