namespace MovieLibrary.Core.Models.Categories.Commands
{
    using System.Collections.Generic;

    public class ListMoviesByFilterQuery
    {
        public string? Text { get; set; }

        public List<int>? CategoryIds { get; set; }

        public decimal? MinRating { get; set; }

        public decimal? MaxRating { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}