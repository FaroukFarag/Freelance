using Freelance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Core.IRepositories
{
    public interface IProposalRepository
    {
        void Add(string freelancerId, int postId);
        IEnumerable<Proposal> PostsProposals();
    }
}