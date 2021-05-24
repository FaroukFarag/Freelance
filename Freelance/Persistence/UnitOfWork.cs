using Freelance.Core;
using Freelance.Core.IRepositories;
using Freelance.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPostRepository Posts { get; private set; }
        public IProposalRepository Proposals { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Posts = new PostRepository(context);
            Proposals = new ProposalRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}