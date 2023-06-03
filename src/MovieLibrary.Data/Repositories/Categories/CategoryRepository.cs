namespace MovieLibrary.Data.Repositories.Categories
{
    using Microsoft.EntityFrameworkCore;
    using MovieLibrary.Data;
    using MovieLibrary.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryRepository : ICategoryRepository
    {
        private readonly MovieLibraryContext _dbContext;

        public CategoryRepository(MovieLibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByMovieIdAsync(int movieId)
        {
            var movieCategories = await _dbContext.MovieCategories
                .Where(mc => mc.MovieId == movieId)
                .ToListAsync();

            var categoryIds = movieCategories.Select(mc => mc.CategoryId).ToList();

            var categories = await _dbContext.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<int> AddAsync(Category entity)
        {
            var createdEntity = await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return createdEntity.Entity.Id;
        }

        public async Task<int> UpdateAsync(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<int> DeleteAsync(Category entity)
        {
            _dbContext.Categories.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }
    }
}