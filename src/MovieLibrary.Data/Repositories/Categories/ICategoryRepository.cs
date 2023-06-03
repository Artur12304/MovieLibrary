namespace MovieLibrary.Data.Repositories.Categories
{
    using MovieLibrary.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetCategoriesByMovieIdAsync(int movieId);
        Task<Category> GetByIdAsync(int id);
        Task<int> AddAsync(Category entity);
        Task<int> UpdateAsync(Category entity);
        Task<int> DeleteAsync(Category entity);
    }
}