using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesOnline.ViewModels
{
    public class CreateTitleViewModel
    {
        [Required]
        public string TitleName { get; set; }
        [Required]
        public string Plot { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
