using Freelance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Core.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientPhotoPath { get; set; }
        public string JobTitle { get; set; }
        public JobType JobType { get; set; }
        public double JobBudget { get; set; }
        public DateTime CreationDate { get; set; }
        public string JobDescription { get; set; }
        public int ProposalsCount { get; set; }
    }
}