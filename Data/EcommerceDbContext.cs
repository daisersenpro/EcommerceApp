using Microsoft.EntityFrameworkCore;
using EcommerceApp.Models;

namespace EcommerceApp.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }

        // DbSet representa las tablas en la BD
        // Las crearemos conforme avancemos
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Orden>? Ordenes { get; set; }
        public DbSet<DetalleOrden>? DetallesOrden { get; set; }
    }
}
