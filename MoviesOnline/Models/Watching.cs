using System;

namespace MoviesOnline.Models
{
    public class Watching
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int  TitleId { get; set; }

        public DateTime StartDate { get; set; }   
    }
}
