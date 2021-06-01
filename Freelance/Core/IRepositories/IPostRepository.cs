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
        IEnumerable<Post> ListPosts();
        IEnumerable<Post> ClientPosts(string clientId);
        IEnumerable<Post> PostsRequests();
    }
}
