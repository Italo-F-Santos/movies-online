using System;

namespace MoviesOnline.Models
{
    public class Title
    {

        public int Id { get; set; }

        public string TitleName { get; set; }

        public string Plot { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
