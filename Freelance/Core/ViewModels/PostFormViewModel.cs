using Freelance.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelance.Core.ViewModels
{
    public class PostFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public JobType JobType { get; set; }

        [Required]
        public double JobBudget { get; set; }

        [Required]
        public string JobDescription { get; set; }
        public bool IsAccepted { get; set; }
    }
}