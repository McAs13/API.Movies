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

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            // Verificar si ya existe una categoría con el mismo nombre
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateUpdateDto.Name);

            if (categoryExists) // Si existe, lanzar una excepción
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateUpdateDto.Name}'");
            }

            // Mapear el DTO a la entidad
            var category = _mapper.Map<Category>(categoryCreateUpdateDto);

            // Crear la categoría en el repositorio
            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new Exception("Ocurrió un error al crear la categoría.");
            }

            //Mapear la entidad creada a DTO
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            // Obtener las categorías del repositorio
            var categories = await _categoryRepository.GetCategoriesAsync();

            // Mapear toda la colección de una vez
            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }


        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            // Obtener la categoría del repositorio
            var category = await _categoryRepository.GetCategoryAsync(id);

            // Mapear la entidad a DTO
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryCreateUpdateDto dto, int id)
        {
            // Verificar si la categoría existe
            var categoryExists = await _categoryRepository.GetCategoryAsync(id);

            if (categoryExists == null) // Si no existe, lanzar una excepción
            {
                throw new InvalidOperationException($"No se encontró la catagoria con ID: '{id}'");
            }

            // Verificar si ya existe una categoría con el mismo nombre (excluyendo la categoría actual)
            var nameExists = await _categoryRepository.CategoryExistsByNameAsync(dto.Name);

            if (nameExists) // Si existe, lanzar una excepción
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{dto.Name}'");
            }

            // Mapear el DTO a la entidad existente
            _mapper.Map(dto, categoryExists);

            // Actualizar la categoría en el repositorio
            var categoryUpdated = await _categoryRepository.UpdateCategoryAsync(categoryExists);

            if (!categoryUpdated)
            {
                throw new Exception("Ocurrió un error al actualizar la categoría.");
            }

            //Retornar el DTO actualizado
            return _mapper.Map<CategoryDto>(categoryExists);
        }
    }
}
