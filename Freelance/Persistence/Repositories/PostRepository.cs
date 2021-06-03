using Freelance.Core.IRepositories;
using Freelance.Core.Models;
using System;
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

        public void RemoveClientPosts(string clientId)
        {
            var clientPosts = _context.Posts.Where(p => p.ClientId == clientId);

            _context.Posts.RemoveRange(clientPosts);
        }

        public IEnumerable<Post> ListPosts(string SearchByTitle, string SearchByDate, string SearchByClientName)
        {
            IQueryable<Post> filteredList = _context.Posts.Where(p => p.IsAccepted);

            if (!String.IsNullOrEmpty(SearchByTitle))
            {
                filteredList = filteredList.Where(p => p.JobTitle.Contains(SearchByTitle));
            }

            if (!String.IsNullOrEmpty(SearchByDate))
            {
                var date = DateTime.Parse(SearchByDate);

                filteredList = filteredList.Where(p => p.CreationDate <= date);
            }

            if (!String.IsNullOrEmpty(SearchByClientName))
            {
                filteredList = filteredList.Where(p => p.Client.FirstName.Contains(SearchByClientName) || p.Client.LastName.Contains(SearchByClientName));
            }

            return filteredList.Include(p => p.Client);
        }

        public IEnumerable<Post> ClientPosts(string clientId, string SearchByTitle, string SearchByDate, string SearchByClientName)
        {
            IQueryable<Post> filteredList = _context.Posts.Where(p => p.ClientId == clientId && p.IsAccepted);

            if (!String.IsNullOrEmpty(SearchByTitle))
            {
                filteredList = filteredList.Where(p => p.JobTitle.Contains(SearchByTitle));
            }

            if (!String.IsNullOrEmpty(SearchByDate))
            {
                var date = DateTime.Parse(SearchByDate);

                filteredList = filteredList.Where(p => p.CreationDate <= date);
            }

            if (!String.IsNullOrEmpty(SearchByClientName))
            {
                filteredList = filteredList.Where(p => p.Client.FirstName.Contains(SearchByClientName) || p.Client.LastName.Contains(SearchByClientName));
            }
            return filteredList.Include(p => p.Client).ToList();
        }

        public IEnumerable<Post> PostsRequests()
        {
            return _context.Posts.Include(p => p.Client).Where(p => !p.IsAccepted).ToList();
        }
    }
}