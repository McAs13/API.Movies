using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using API.Movies.Repository.IRepository;
using API.Movies.Services.IServices;
using AutoMapper;

namespace API.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto)
        {
            // Verificar si una película con el mismo nombre ya existe
            var movieExists = await _movieRepository.MovieExistsByNameAsync(movieCreateUpdateDto.Name);

            if (movieExists)
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre de '{movieCreateUpdateDto.Name}'");
            }

            // Mapear el DTO de creación a la entidad de película
            var movie = _mapper.Map<Movie>(movieCreateUpdateDto);

            // Crear la película en el repositorio
            var movieCreated = await _movieRepository.CreateMovieAsync(movie);

            if (!movieCreated)
            {
                throw new Exception("Ocurrió un error al crear la película.");
            }

            // Mapear la entidad de película creada al DTO de película
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            // Verificar si la película existe
            var movieExists = await _movieRepository.GetMovieAsync(id);

            if (movieExists == null) // Si no existe, lanzar una excepción
            {
                throw new InvalidOperationException($"La película con ID '{id}' no existe.");
            }

            // Eliminar la película del repositorio
            var movieDeleted = await _movieRepository.DeleteMovieAsync(id);

            if (!movieDeleted)
            {
                throw new Exception("Ocurrió un error al eliminar la película.");
            }

            return movieDeleted;
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            // Obtener la película del repositorio
            var movie = await _movieRepository.GetMovieAsync(id);

            if (movie == null)
            {
                throw new InvalidOperationException($"La película con ID '{id}' no existe.");
            }

            // Mapear la entidad de película al DTO de película
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            // Obtener todas las películas del repositorio
            var movies = await _movieRepository.GetMoviesAsync();

            // Mapear la lista de entidades de película a una lista de DTOs de película
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public Task<bool> MovieExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto dto, int id)
        {
            // Verificar si la película existe
            var movieExists = await _movieRepository.GetMovieAsync(id);

            if (movieExists == null) // Si no existe, lanzar una excepción
            {
                throw new InvalidOperationException($"La película con ID '{id}' no existe.");
            }

            // Verificar si una película con el mismo nombre ya existe (excluyendo la película actual)
            var nameExists = await _movieRepository.MovieExistsByNameAsync(dto.Name);

            if (nameExists && !string.Equals(movieExists.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre de '{dto.Name}'");
            }

            // Mapear el DTO de actualización a la entidad de película existente
            _mapper.Map(dto, movieExists);

            // Actualizar la película en el repositorio
            var movieUpdated = await _movieRepository.UpdateMovieAsync(movieExists);

            if (!movieUpdated)
            {
                throw new Exception("Ocurrió un error al actualizar la película.");
            }

            // Retornar el DTO de película actualizado
            return _mapper.Map<MovieDto>(movieExists);
        }
    }
}
