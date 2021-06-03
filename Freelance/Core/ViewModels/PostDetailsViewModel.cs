using Freelance.Core.Enums;
using Freelance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Core.ViewModels
{
    public class PostDetailsViewModel
    {
        public PostViewModel Post { get; set; }

        public ILookup<int, Proposal> Proposals { get; set; }

    }
}