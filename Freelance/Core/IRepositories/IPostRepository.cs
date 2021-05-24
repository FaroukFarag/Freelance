using Freelance.Core.Models;
using System.Collections.Generic;

namespace Freelance.Core.IRepositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        IEnumerable<Post> ListPosts();
        IEnumerable<Post> PostsRequests();
        void Accept(int id);
    }
}
