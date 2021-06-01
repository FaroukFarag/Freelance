using Freelance.Core.Models;
using System.Collections.Generic;

namespace Freelance.Core.IRepositories
{
    public interface IProposalRepository
    {
        Proposal GetProposal(string freelancerId, int postId);
        void Add(string freelancerId, int postId);
        void Accept(string freelancerId, int postId);
        void Remove(string freelancerId, int postId);
        IEnumerable<Proposal> PostsProposals();
    }
}