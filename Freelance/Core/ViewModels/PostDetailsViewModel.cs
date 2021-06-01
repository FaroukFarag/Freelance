using Freelance.Core.Enums;
using Freelance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Core.ViewModels
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public JobType JobType { get; set; }
        public double JobBudget { get; set; }
        public DateTime CreationDate { get; set; }
        public string JobDescription { get; set; }

        public ApplicationUser Client { get; set; }
        public ILookup<string, Proposal> Proposals { get; set; }
    }
}