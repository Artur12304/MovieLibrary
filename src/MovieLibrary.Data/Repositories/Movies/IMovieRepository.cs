namespace MovieLibrary.Data.Repositories.Movies
{
    using MovieLibrary.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<List<Movie>> GetMoviesByFilterAsync(string text, List<int> categoryIds, decimal? minRating, decimal? maxRating, int? page, int? pageSize);
        Task<int> GetTotalCountAsync(string text, List<int> categoryIds, decimal? minRating, decimal? maxRating);
        Task<Movie> GetByIdAsync(int id);
        Task<int> AddAsync(Movie entity);
        Task<int> UpdateAsync(Movie entity);
        Task<int> DeleteAsync(Movie entity);
    }
}