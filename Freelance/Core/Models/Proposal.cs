using System.ComponentModel.DataAnnotations.Schema;

namespace Freelance.Core.Models
{
    public class Proposal
    {
        //[ForeignKey("Freelancer")]
        public string FreelancerId { get; set; }
        public int PostId { get; set; }
        public bool IsAccepted { get; set; }

        public ApplicationUser Freelancer { get; set; }
        public Post Post { get; set; }
    }
}