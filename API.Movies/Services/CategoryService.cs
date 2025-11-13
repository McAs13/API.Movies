using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using API.Movies.Repository.IRepository;
using API.Movies.Services.IServices;
using AutoMapper;

namespace API.Movies.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryCreateDTO categoryCreateDTO)
        {
            //Validar si la categoría ya existe
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDTO.Name);

            if (categoryExists)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateDTO.Name}'");
            }

            //Mapear el DTO a la entidad
            var category = _mapper.Map<Category>(categoryCreateDTO);

            //Crear la categoría en el repositorio
            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new Exception("Ocurrió un error al crear la categoría.");
            }

            //Mapear la entidad creada a DTO
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategoryDTO>> GetCategoriesAsync()
        {
            // Obtener las categorías del repositorio
            var categories = await _categoryRepository.GetCategoriesAsync();

            // Mapear toda la colección de una vez
            return _mapper.Map<ICollection<CategoryDTO>>(categories);
        }


        public async Task<CategoryDTO> GetCategoryAsync(int id)
        {
            // Obtener la categoría del repositorio
            var category = await _categoryRepository.GetCategoryAsync(id);

            // Mapear toda la colección de una vez
            return _mapper.Map<CategoryDTO>(category);
        }

        public Task<CategoryDTO> UpdateCategoryAsync(int id, Category categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
