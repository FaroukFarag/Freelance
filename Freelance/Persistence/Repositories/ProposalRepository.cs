using Freelance.Core.IRepositories;
using Freelance.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Freelance.Persistence.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly ApplicationDbContext _context;

        public ProposalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Proposal GetProposal(string freelancerId, int postId)
        {
            return _context.Proposals.SingleOrDefault(p => p.FreelancerId == freelancerId && p.PostId == postId);
        }
        public void Add(string freelancerId, int postId)
        {
            var proposal = new Proposal
            {
                FreelancerId = freelancerId,
                PostId = postId
            };

            _context.Proposals.Add(proposal);
        }

        public void Accept(string freelancerId, int postId)
        {
            var proposal = _context.Proposals.Single(p => p.FreelancerId == freelancerId && p.PostId == postId);

            proposal.IsAccepted = true;
        }

        public void Remove(string freelancerId, int postId)
        {
            var proposal = GetProposal(freelancerId, postId);

            _context.Proposals.Remove(proposal);
        }

        public IEnumerable<Proposal> PostsProposals()
        {
            return _context.Proposals.Where(p => !p.IsAccepted).Include(p => p.Freelancer).Include(p => p.Post).ToList();
        }
    }
}