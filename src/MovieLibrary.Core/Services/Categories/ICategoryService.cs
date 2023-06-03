namespace MovieLibrary.Core.Interface.Services.Categories
{
    using MovieLibrary.Core.Models.Categories.Commands;
    using MovieLibrary.Core.Models.Categories.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<int> CreateCategoryAsync(CreateCategoryCommand command);
        Task<int> UpdateCategoryAsync(UpdateCategoryCommand command);
        Task<int> DeleteCategoryAsync(int id);
    }
}