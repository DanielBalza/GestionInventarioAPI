using Microsoft.EntityFrameworkCore;
using GestionInventarioAPI.Models; // Importamos nuestros modelos para definir las tablas de la base de datos

namespace GestionInventarioAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Esta linea le dice a EF que cree una tabla llamada "productos"
        public DbSet<Producto> Productos { get; set; }
    }
}
