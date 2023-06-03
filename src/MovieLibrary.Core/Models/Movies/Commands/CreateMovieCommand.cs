namespace MovieLibrary.Core.Models.Movies.Commands
{
    using System.Collections.Generic;

    public class CreateMovieCommand
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}