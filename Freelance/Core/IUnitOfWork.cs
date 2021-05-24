using Freelance.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Core
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        IProposalRepository Proposals { get; }

        void Complete();
    }
}
