using Freelance.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelance.Core.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public JobType JobType { get; set; }

        [Required]
        public double JobBudget { get; set; }

        [Required]
        public string JobDescription { get; set; }
    }
}