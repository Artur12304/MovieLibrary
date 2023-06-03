namespace MovieLibrary.Core.Models.Movies.Commands
{
    using System.Collections.Generic;

    public class UpdateMovieCommand
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
