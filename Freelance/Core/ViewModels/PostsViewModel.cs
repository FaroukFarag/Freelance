using Freelance.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Freelance.Core.ViewModels
{
    public class PostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public ILookup<string, Proposal> Proposals { get; set; }
    }
}