using Freelance.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Freelance.Persistence.EntityConfigurations
{
    public class PostConfigurations : EntityTypeConfiguration<Post>
    {
        public PostConfigurations()
        {
            Property(p => p.ClientName)
                .IsRequired();

            Property(p => p.JobType)
                .IsRequired();

            Property(p => p.JobBudget)
                .IsRequired();

            Property(p => p.CreationDate)
                .IsRequired();

            Property(p => p.JobDescription)
                .IsRequired()
                .HasMaxLength(255);

            HasOptional(p => p.Proposals)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }
    }
}