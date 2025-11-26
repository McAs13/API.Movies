using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la película es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre de la película no puede exceder los 100 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La duración de la película es obligatoria.")]
        public int Duration { get; set; }
        [MaxLength(1000, ErrorMessage = "La descripción de la película no puede exceder los 1000 caracteres.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "La clasificación de la película es obligatoria.")]
        [MaxLength(10, ErrorMessage = "La clasificación de la película no puede exceder los 10 caracteres.")]
        public string Clasification { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
