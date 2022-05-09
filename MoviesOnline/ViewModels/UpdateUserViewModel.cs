using System.ComponentModel.DataAnnotations;

namespace MoviesOnline.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserMiddleName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
