using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelance.Core.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The First Name must be less 100 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The Last Name must be less 100 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string PhotoPath { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}