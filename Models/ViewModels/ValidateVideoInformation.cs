using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoUploaderWebApp.Models.ViewModels
{
    public class ValidateVideoInformation
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "*")]
        public string VideoTitle { get; set; }

        [Required(ErrorMessage = "*")]
        public string About { get; set; }

        [Required(ErrorMessage = "*")]
        public string Tags { get; set; }
        public string VideoLocation { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(1, int.MaxValue, ErrorMessage = "Must select a language")]
        public string Language { get; set; }

        public List<string> Category { get; set; }
    }
}