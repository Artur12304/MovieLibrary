namespace MovieLibrary.Core.Interface.Services.Categories
{
    using AutoMapper;
    using MovieLibrary.Core.Models.Categories.Commands;
    using MovieLibrary.Core.Models.Categories.Model;
    using MovieLibrary.Data.Entities;
    using MovieLibrary.Data.Repositories.Categories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new ArgumentException($"Category with Id = {id} not found.");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<int> CreateCategoryAsync(CreateCategoryCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var newCategory = _mapper.Map<Category>(command);

            var createdId = await _categoryRepository.AddAsync(newCategory);

            return createdId;
        }

        public async Task<int> UpdateCategoryAsync(UpdateCategoryCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var category = await _categoryRepository.GetByIdAsync(command.Id);

            if (category is null)
            {
                throw new ArgumentException($"Category with Id = {command.Id} not found.");
            }

            _mapper.Map(command, category);

            await _categoryRepository.UpdateAsync(category);

            return category.Id;
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new ArgumentException($"Category with Id = {id} not found.");
            }

            var deletedId = await _categoryRepository.DeleteAsync(category);

            return deletedId;
        }
    }
}
