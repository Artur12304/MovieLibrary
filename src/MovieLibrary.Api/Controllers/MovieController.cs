namespace MovieLibrary.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieLibrary.Core.Interface.Services.Movies;
    using MovieLibrary.Core.Models.Categories.Commands;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/v1/Movie/Filter")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<ListMoviesByFilterResponse>> GetMoviesByFilter(
            [FromQuery] string text,
            [FromQuery] List<int> categoryIds,
            [FromQuery] decimal? minRating,
            [FromQuery] decimal? maxRating,
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            var query = new ListMoviesByFilterQuery
            {
                Text = text,
                CategoryIds = categoryIds,
                MinRating = minRating,
                MaxRating = maxRating,
                Page = page,
                PageSize = pageSize
            };

            var movies = await _movieService.GetMoviesByFilterAsync(query);
            return Ok(movies);
        }
    }
}