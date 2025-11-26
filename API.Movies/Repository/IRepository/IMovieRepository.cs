using API.Movies.DAL.Models;

namespace API.Movies.Repository.IRepository
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetMoviesAsync(); // Retorna una lista de películas (todas las películas)
        Task<Movie> GetMovieAsync(int id); // Retorna una película por su ID
        Task<bool> MovieExistsByIdAsync(int id); // Verifica si una película existe por su ID
        Task<bool> MovieExistsByNameAsync(string name); // Verifica si una película existe por su nombre
        Task<bool> CreateMovieAsync(Movie movie); // Crea una nueva película
        Task<bool> UpdateMovieAsync(Movie movie); // Actualiza una película existente -- Puedo actualizar el nombre, duración, descripción, clasificación y la fecha de actualización
        Task<bool> DeleteMovieAsync(int id); // Elimina una película existente
    }
}
