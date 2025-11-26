using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre de la película")]
        public string Name { get; set; } = null!; // No vacio, maximo 100 caracteres
        [Required]
        [Display(Name = "Duración en minutos")]
        public int Duration { get; set; } // Duracion en minutos
        [Required]
        [Display(Name = "Descripción de la película")]
        public string Description { get; set; } = null!; // No vacio
        [Required]
        [MaxLength(10)]
        [Display(Name = "Clasificación de la película")]
        public string Clasification { get; set; } = null!; // No vacio, maximo 100 caracteres (PG, PG-13, R, etc)
    }
}
