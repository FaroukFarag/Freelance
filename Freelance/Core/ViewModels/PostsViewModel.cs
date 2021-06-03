using Freelance.Core.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace Freelance.Core.ViewModels
{
    public class PostsViewModel
    {
        public IPagedList<PostViewModel> Posts { get; set; }
        public string SearchByTitle { get; set; }
        public string SearchByDate { get; set; }
        public string SearchByClientName { get; set; }

        public ILookup<int, Proposal> Proposals { get; set; }
    }
}