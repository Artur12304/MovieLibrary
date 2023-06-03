namespace MovieLibrary.Core.Interface.Services.Movies
{
    using AutoMapper;
    using MovieLibrary.Core.Models.Categories.Commands;
    using MovieLibrary.Core.Models.Categories.Model;
    using MovieLibrary.Core.Models.Movies.Commands;
    using MovieLibrary.Core.Models.Movies.Model;
    using MovieLibrary.Data.Entities;
    using MovieLibrary.Data.Repositories.Categories;
    using MovieLibrary.Data.Repositories.Movies;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MovieService(IMapper mapper,
            IMovieRepository movieRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();

            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);

            foreach (var movieDto in movieDtos)
            {
                var categories = await _categoryRepository.GetCategoriesByMovieIdAsync(movieDto.Id);
                movieDto.Categories = _mapper.Map<List<CategoryDto>>(categories);
            }

            return movieDtos;
        }

        public async Task<ListMoviesByFilterResponse> GetMoviesByFilterAsync(ListMoviesByFilterQuery query)
        {
            var movies = await _movieRepository.GetMoviesByFilterAsync(query.Text,
                query.CategoryIds, query.MinRating, query.MaxRating, query.Page, query.PageSize);

            var movieDtos = _mapper.Map<List<MovieDto>>(movies).OrderByDescending(x => x.ImdbRating).ToList();

            foreach (var movieDto in movieDtos)
            {
                var categories = await _categoryRepository.GetCategoriesByMovieIdAsync(movieDto.Id);
                movieDto.Categories = _mapper.Map<List<CategoryDto>>(categories);
            }

            var totalCount = await _movieRepository.GetTotalCountAsync(query.Text,
                query.CategoryIds, query.MinRating, query.MaxRating);

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize.GetValueOrDefault(10));

            var response = new ListMoviesByFilterResponse
            {
                Result = movieDtos,
                CurrentPage = query.Page.GetValueOrDefault(1),
                PageCount = totalPages,
                PageSize = query.PageSize.GetValueOrDefault(10),
                RowCount = totalCount
            };

            return response;
        }


        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie is null)
            {
                throw new ArgumentException($"Movie with Id = {id} not found.");
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            var categories = await _categoryRepository.GetCategoriesByMovieIdAsync(movieDto.Id);
            movieDto.Categories = _mapper.Map<List<CategoryDto>>(categories);

            return movieDto;
        }

        public async Task<int> CreateMovieAsync(CreateMovieCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var newMovie = _mapper.Map<Movie>(command);
            newMovie.MovieCategories = new List<MovieCategory>();

            foreach (var categoryId in command.CategoryIds)
            {
                var movieCategory = new MovieCategory
                {
                    CategoryId = categoryId
                };

                newMovie.MovieCategories.Add(movieCategory);
            }

            var createdId = await _movieRepository.AddAsync(newMovie);

            return createdId;
        }

        public async Task<int> UpdateMovieAsync(UpdateMovieCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var movie = await _movieRepository.GetByIdAsync(command.Id);

            if (movie is null)
            {
                throw new ArgumentException($"Movie with Id = {command.Id} not found.");
            }

            _mapper.Map(command, movie);

            movie.MovieCategories.Clear();
            foreach (var categoryId in command.CategoryIds)
            {
                var movieCategory = new MovieCategory
                {
                    MovieId = movie.Id,
                    CategoryId = categoryId
                };

                movie.MovieCategories.Add(movieCategory);
            }

            await _movieRepository.UpdateAsync(movie);

            return movie.Id;
        }

        public async Task<int> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie is null)
            {
                throw new ArgumentException($"Movie with Id = {id} not found.");
            }

            var deletedId = await _movieRepository.DeleteAsync(movie);

            return deletedId;
        }
    }
}
