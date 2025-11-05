using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models
{
    public class AuditBase
    {
        [Key] //Este data annotation indica que esta propiedad es la clave primaria de la entidad
        public virtual int Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifyDate { get; set; }
    }
}
