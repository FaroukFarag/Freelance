using Freelance.Core.Enums;
using System;

namespace Freelance.Core.ViewModels
{
    public class PostRequestViewModel
    {
        public int Id { get; set; }
        public string ClientPhotoPath { get; set; }
        public string JobTitle { get; set; }
        public JobType JobType { get; set; }
        public double JobBudget { get; set; }
        public DateTime CreationDate { get; set; }
    }
}