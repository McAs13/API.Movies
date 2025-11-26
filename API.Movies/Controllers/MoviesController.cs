using API.Movies.DAL.Models.Dtos;
using API.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet(Name = "GetMoviesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<MovieDto>>> GetMoviesAsync()
        {
            var moviesDto = await _movieService.GetMoviesAsync();
            return Ok(moviesDto);
        }

        [HttpGet("{id:int}", Name = "GetMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> GetMovieAsync(int id)
        {
            try
            {
                var movieDto = await _movieService.GetMovieAsync(id);
                return Ok(movieDto);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("no existe"))
            {
                return NotFound(new { ex.Message }); // Devolvemos un 404 Not Found si no se encuentra la película a actualizar
            }
        }

        [HttpPost(Name = "CreateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieCreateUpdateDto movieCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var movieDto = await _movieService.CreateMovieAsync(movieCreateDto);
                return CreatedAtRoute("GetMovieAsync", new { id = movieDto.Id }, movieDto);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message }); // Devolvemos un 409 Conflict si ya existe una película con el mismo nombre
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> UpdateMovieAsync([FromBody] MovieCreateUpdateDto dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedMovie = await _movieService.UpdateMovieAsync(dto, id); // Llamamos al servicio para actualizar la película
                return Ok(updatedMovie); // Devolvemos un 200 OK con la película actualizada
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message }); // Devolvemos un 409 Conflict si ya existe una película con el mismo nombre
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("no existe"))
            {
                return NotFound(new { ex.Message }); // Devolvemos un 404 Not Found si no se encuentra la película a actualizar
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteMovieAsync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                var movieDeleted = await _movieService.DeleteMovieAsync(id);
                return Ok(movieDeleted);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("no existe"))
            {
                return NotFound(new { ex.Message }); // Devolvemos un 404 Not Found si no se encuentra la película a eliminar
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
