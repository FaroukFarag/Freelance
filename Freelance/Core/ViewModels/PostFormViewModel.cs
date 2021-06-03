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
        public string ClientId { get; set; }
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [Display(Name = "Job Budget")]
        public double JobBudget { get; set; }
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        
        [Required]
        [Display(Name = "Job Type")]
        public JobType JobType { get; set; }
        public bool IsAccepted { get; set; }
    }
}