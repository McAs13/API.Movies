using API.Movies.DAL.Models.Dtos;
using API.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategoriesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategoriesAsync()
        {
            var categoriesDto = await _categoryService.GetCategoriesAsync();
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            try
            {
                var categoryDto = await _categoryService.GetCategoryAsync(id);
                return Ok(categoryDto);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message }); // Devolvemos un 404 Not Found si no se encuentra la categoría a actualizar
            }
        }

        [HttpPost(Name = "CreateCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CategoryCreateUpdateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDto); // Llamamos al servicio para crear la categoría

                // Vamos a devolver un 201 Created con la ruta para obtener la categoría creada
                return CreatedAtRoute(
                    "GetCategoryAsync",                 // 1er parámetro: el nombre de la ruta
                    new { id = createdCategory.Id },    // 2º parámetro: los valores para los parámetros de la ruta
                    createdCategory                     // 3er parámetro: el contenido que devolvemos en el body
                    );
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync([FromBody] CategoryCreateUpdateDto dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(dto, id); // Llamamos al servicio para actualizar la categoría
                return Ok(updatedCategory); // Devolvemos un 200 OK con la categoría actualizada
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message }); // Devolvemos un 409 Conflict si ya existe una categoría con el mismo nombre
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message }); // Devolvemos un 404 Not Found si no se encuentra la categoría a actualizar
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var deletedCategory = await _categoryService.DeleteCategoryAsync(id); // Llamamos al servicio para borrar la categoría
                return Ok(deletedCategory); // Retorno 200 OK si se ha borrado correctamente
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message }); // Devolvemos un 404 Not Found si no se encuentra la categoría a eliminar
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}