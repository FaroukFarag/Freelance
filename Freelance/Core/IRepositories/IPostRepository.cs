using Freelance.Core.Models;
using System.Collections.Generic;

namespace Freelance.Core.IRepositories
{
    public interface IPostRepository
    {
        Post GetPost(int id);
        void Add(Post post);
        void Update(Post post);
        void Accept(int id);
        void Remove(Post post);
        void RemoveClientPosts(string clientId);
        IEnumerable<Post> ListPosts(string SearchByTitle, string SearchByDate, string SearchByClientName);
        IEnumerable<Post> ClientPosts(string clientId, string SearchByTitle, string SearchByDate, string SearchByClientName);
        IEnumerable<Post> PostsRequests();
    }
}
