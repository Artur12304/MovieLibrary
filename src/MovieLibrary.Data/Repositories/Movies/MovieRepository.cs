namespace MovieLibrary.Data.Repositories.Movies
{
    using Microsoft.EntityFrameworkCore;
    using MovieLibrary.Data;
    using MovieLibrary.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MovieRepository : IMovieRepository
    {
        private readonly MovieLibraryContext _dbContext;

        public MovieRepository(MovieLibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<List<Movie>> GetMoviesByFilterAsync(string text, List<int> categoryIds,
            decimal? minRating, decimal? maxRating, int? page, int? pageSize)
        {
            var moviesQuery = _dbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(text))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(text));
            }

            if (categoryIds != null && categoryIds.Any())
            {
                moviesQuery = moviesQuery.Where(m => m.MovieCategories.Any(mc => categoryIds.Contains(mc.CategoryId)));
            }

            if (minRating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.ImdbRating >= minRating.Value);
            }

            if (maxRating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.ImdbRating <= maxRating.Value);
            }

            int pageNumber = page.HasValue ? page.Value : 1;
            int pageSizeNumber = pageSize.HasValue ? pageSize.Value : 10;

            moviesQuery = moviesQuery.Skip((pageNumber - 1) * pageSizeNumber).Take(pageSizeNumber);

            var movies = await moviesQuery.ToListAsync();

            return movies;
        }

        public async Task<int> GetTotalCountAsync(string text, List<int> categoryIds, decimal? minRating, decimal? maxRating)
        {
            var moviesQuery = _dbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(text))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(text));
            }

            if (categoryIds != null && categoryIds.Any())
            {
                moviesQuery = moviesQuery.Where(m => m.MovieCategories.Any(mc => categoryIds.Contains(mc.CategoryId)));
            }

            if (minRating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.ImdbRating >= minRating.Value);
            }

            if (maxRating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.ImdbRating <= maxRating.Value);
            }

            var totalCount = await moviesQuery.CountAsync();

            return totalCount;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _dbContext.Movies.FindAsync(id);
        }

        public async Task<int> AddAsync(Movie entity)
        {
            var createdEntity = await _dbContext.Movies.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return createdEntity.Entity.Id;
        }

        public async Task<int> UpdateAsync(Movie entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<int> DeleteAsync(Movie entity)
        {
            _dbContext.Movies.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }
    }
}