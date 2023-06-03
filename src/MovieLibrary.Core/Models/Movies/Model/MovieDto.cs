namespace MovieLibrary.Core.Models.Movies.Model
{
    using MovieLibrary.Core.Models.Categories.Model;
    using System.Collections.Generic;

    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
