using Freelance.Core.Models;
using Freelance.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Freelance.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostConfigurations());
            modelBuilder.Configurations.Add(new ProposalConfigurations());

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOptional(u => u.Proposal)
            //    .WithRequired(p => p.Freelancer)
            //    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}