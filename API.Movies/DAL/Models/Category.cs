using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models
{
    public class Category : AuditBase
    {
        [Required] // Este data annotation indica que esta propiedad es obligatoria
        [Display(Name = "Nombre de la categoría")] // Este data annotation especifica el nombre que se mostrará en las vistas o formularios
        public string Name { get; set; }
    }
}
