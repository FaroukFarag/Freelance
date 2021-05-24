namespace Freelance.Core.Models
{
    public class Proposal
    {
        public string FreelancerId { get; set; }

        public int PostId { get; set; }

        public ApplicationUser Freelancer { get; set; }
        public Post Post { get; set; }
    }
}