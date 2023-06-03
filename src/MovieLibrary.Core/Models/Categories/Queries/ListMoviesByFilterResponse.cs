namespace MovieLibrary.Core.Models.Categories.Commands
{
    using MovieLibrary.Core.Models.Movies.Model;
    using System.Collections.Generic;

    public class ListMoviesByFilterResponse
    {
        public virtual ICollection<MovieDto> Result { get; set; }
        public virtual int CurrentPage { get; set; }
        public virtual int PageCount { get; set; }
        public virtual int PageSize { get; set; }
        public virtual long RowCount { get; set; }
    }
}