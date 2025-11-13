using API.Movies.DAL.Models.Dtos;
using API.Movies.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Movies.Controllers
{
    [Route("api/[controller]")] // Este atributo define la ruta base para el controlador (api/categories)
    [ApiController] // Este atributo indica que este controlador es un controlador de API
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService; // Inyección de dependencia del servicio de categorías
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet] // Este atributo indica que este método responde a solicitudes HTTP GET
        [ProducesResponseType(StatusCodes.Status200OK)] // Indica que el método puede retornar un código 200 OK
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Indica que el método puede retornar un código 500 Internal Server Error
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Indica que el método puede retornar un código 400 Bad Request
        public async Task<ActionResult<ICollection<CategoryDTO>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync(); // Llama al servicio para obtener las categorías
            return Ok(categories); // Retorna un código 200 OK con la lista de categorías
        }
    }
}
