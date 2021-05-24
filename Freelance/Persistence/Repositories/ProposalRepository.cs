using Freelance.Core.IRepositories;
using Freelance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Persistence.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly ApplicationDbContext _context;

        public ProposalRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public IEnumerable<Proposal> PostsProposals()
        {
            return _context.Proposals.ToList();
        }
    }
}