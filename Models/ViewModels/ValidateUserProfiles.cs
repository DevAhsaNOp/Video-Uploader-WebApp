using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoUploaderWebApp.Models.ViewModels
{
    public class ValidateUserProfiles
    {
        [Required(ErrorMessage ="*")]
        [EmailAddress(ErrorMessage ="Email is not valid!")]
        public string Email { get; set; }

        [Required(ErrorMessage ="*")]
        public string Password { get; set; }

        public string Username { get; set; }
    }
}