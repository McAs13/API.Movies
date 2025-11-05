using API.Movies.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Movies.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Sección para crear el DbSet de cada una de las entidades del modelo
        public DbSet<Category> Categories { get; set; }
    }
}
