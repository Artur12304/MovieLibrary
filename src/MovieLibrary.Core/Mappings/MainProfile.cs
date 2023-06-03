namespace MovieLibrary.Core.Mappings
{
    using AutoMapper;
    using MovieLibrary.Core.Models.Categories.Commands;
    using MovieLibrary.Core.Models.Categories.Model;
    using MovieLibrary.Core.Models.Movies.Commands;
    using MovieLibrary.Core.Models.Movies.Model;
    using MovieLibrary.Data.Entities;

    public class MainProfile : Profile
    {
        public MainProfile()
        {
            // Movie Mapping
            CreateMap<Movie, MovieDto>();

            CreateMap<MovieDto, Movie>();

            CreateMap<CreateMovieCommand, Movie>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateMovieCommand, Movie>();

            // Category Mapping
            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>();

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
