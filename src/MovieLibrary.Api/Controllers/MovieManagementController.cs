namespace MovieLibrary.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieLibrary.Core.Interface.Services.Movies;
    using MovieLibrary.Core.Models.Movies.Commands;
    using MovieLibrary.Core.Models.Movies.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/v1/MovieManagement")]
    public class MovieManagementController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieManagementController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieCommand command)
        {
            var createdId = await _movieService.CreateMovieAsync(command);
            return Ok(createdId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateMovie(UpdateMovieCommand command)
        {
            var updatedId = await _movieService.UpdateMovieAsync(command);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteMovie(int id)
        {
            var deletedId = await _movieService.DeleteMovieAsync(id);
            return Ok(deletedId);
        }
    }
}