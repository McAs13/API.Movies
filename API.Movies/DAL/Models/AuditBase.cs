using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models
{
    public class AuditBase
    {
        [Key] //Este data annotation indica que esta propiedad es la clave primaria de la entidad
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
    }
}
