using API.Movies.DAL;
using API.Movies.DAL.Models;
using API.Movies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Movies.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking() // Mejora el rendimiento al no rastrear los cambios en las entidades
                .AnyAsync(c => c.Id == id); // Verifica si existe alguna categoría con el ID proporcionado
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _context.Categories
                .AsNoTracking() // Mejora el rendimiento al no rastrear los cambios en las entidades
                .AnyAsync(c => c.Name.ToLower() == name.ToLower()); // Verifica si existe alguna categoría con el nombre proporcionado (ignorando mayúsculas/minúsculas)
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            category.CreateDate = DateTime.UtcNow; // Establece la fecha de creación en UTC
            await _context.Categories.AddAsync(category); // Agrega la nueva categoría al contexto de la base de datos
            return await SaveAsync(); // Guarda los cambios en la base de datos y retorna true si se guardaron correctamente

        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id); // Obtiene la categoría por su ID

            if (category == null)
            {
                return false; // Retorna false si la categoría no existe
            }
            _context.Categories.Remove(category); // Elimina la categoría del contexto de la base de datos
            return await SaveAsync(); // Guarda los cambios en la base de datos y retorna true si se guardaron correctamente
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking() // Mejora el rendimiento al no rastrear los cambios en las entidades
                .OrderBy(c => c.Name) // Ordena las categorías por nombre de forma ascendente
                .ToListAsync(); // Retorna todas las categorías como una lista de forma asíncrona

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id) // Al ser un método asíncrono, debería usar async/await
        {
            return await _context.Categories
                .AsNoTracking() // AsNoTracking() mejora el rendimiento al no rastrear los cambios en la entidad
                .FirstOrDefaultAsync(c => c.Id == id); // Expresión lambda para buscar la categoría por su ID - Select * from Categories where Id = id 
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            category.ModifiedDate = DateTime.UtcNow; // Establece la fecha de modificación en UTC
            _context.Categories.Update(category); // Actualiza la categoría en el contexto de la base de datos
            return await SaveAsync(); // Guarda los cambios en la base de datos y retorna true si se guardaron correctamente
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false; // Guarda los cambios en la base de datos y retorna true si se guardaron correctamente
        }
    }
}
