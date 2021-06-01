using Freelance.Core.IRepositories;
using Freelance.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Freelance.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Include(p => p.Client).Include(p => p.Proposals.Select(pro => pro.Post)).SingleOrDefault(p => p.Id == id);
        }
        public void Add(Post post)
        {
            _context.Posts.Add(post);
        }

        public void Update(Post post)
        {
            var oldPost = GetPost(post.Id);

            _context.Entry(oldPost).CurrentValues.SetValues(post);
        }

        public void Accept(int id)
        {
            var post = _context.Posts.Single(p => p.Id == id);

            post.IsAccepted = true;
        }

        public void Remove(Post post)
        {
            _context.Posts.Remove(post);
        }

        public IEnumerable<Post> ListPosts()
        {
            return _context.Posts.Include(p => p.Client).Where(p => p.IsAccepted).ToList();
        }

        public IEnumerable<Post> ClientPosts(string clientId)
        {
            return _context.Posts.Include(p => p.Client).Where(p => p.ClientId == clientId && p.IsAccepted).ToList();
        }

        public IEnumerable<Post> PostsRequests()
        {
            return _context.Posts.Include(p => p.Client).Where(p => !p.IsAccepted).ToList();
        }
    }
}