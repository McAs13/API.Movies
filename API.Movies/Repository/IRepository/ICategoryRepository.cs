using API.Movies.DAL.Models;

namespace API.Movies.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync(); // Retorna una lista de categorías (todas las categorías)
        Task<Category> GetCategoryByIdAsync(int id); // Retorna una categoría por su ID
        Task<bool> CategoryExistsByIdAsync(int id); // Verifica si una categoría existe por su ID
        Task<bool> CategoryExistsByNameAsync(string name); // Verifica si una categoría existe por su nombre
        Task<bool> CreateCategoryAsync(Category category); // Crea una nueva categoría
        Task<bool> UpdateCategoryAsync(Category category); // Actualiza una categoría existente -- Puedo actualizar el nombre de la categoría y la fecha de actualización
        Task<bool> DeleteCategoryAsync(int id); // Elimina una categoría existente

    }
}
