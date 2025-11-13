using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;

namespace API.Movies.Services.IServices
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetCategoryAsync(int id);
        Task<CategoryDTO> CreateCategoryAsync(CategoryCreateDTO categoryDto);
        Task<CategoryDTO> UpdateCategoryAsync(int id, Category categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsByIdAsync(int id);
        Task<bool> CategoryExistsByNameAsync(string name);
    }
}
