using Freelance.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Freelance.Persistence.EntityConfigurations
{
    public class ProposalConfigurations : EntityTypeConfiguration<Proposal>
    {
        public ProposalConfigurations()
        {
            HasKey(p => new { p.FreelancerId, p.PostId });

            Property(p => p.FreelancerId)
                .IsRequired();

            Property(p => p.PostId)
                .IsRequired();
            
            HasRequired(p => p.Freelancer)
                .WithMany()
                .WillCascadeOnDelete(true);

            HasRequired(p => p.Post)
                .WithMany(p => p.Proposals)
                .WillCascadeOnDelete(true);
        }
    }
}