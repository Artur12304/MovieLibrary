namespace MovieLibrary.Core.Interface.Services.Movies
{
    using MovieLibrary.Core.Models.Categories.Commands;
    using MovieLibrary.Core.Models.Movies.Commands;
    using MovieLibrary.Core.Models.Movies.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<ListMoviesByFilterResponse> GetMoviesByFilterAsync(ListMoviesByFilterQuery query);
        Task<MovieDto> GetMovieByIdAsync(int id);
        Task<int> CreateMovieAsync(CreateMovieCommand command);
        Task<int> UpdateMovieAsync(UpdateMovieCommand command);
        Task<int> DeleteMovieAsync(int id);
    }
}