using Freelance.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freelance.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public JobType JobType { get; set; }
        public double JobBudget { get; set; }
        public DateTime CreationDate { get; set; }
        public string JobDescription { get; set; }
        public bool IsAccepted { get; set; }
        public string ClientId { get; set; }

        public ICollection<Proposal> Proposals { get; set; }
        public ApplicationUser Client { get; set; }
    }
}