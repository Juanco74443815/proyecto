using Microsoft.EntityFrameworkCore;
using MicroservicioDemo2.Models;

namespace MicroservicioDemo2.Data
{
    public class ProductoContext : DbContext
    {
        // Constructor que recibe las opciones de configuración para el DbContext
        public ProductoContext(DbContextOptions<ProductoContext> options) : base(options) { }
    
        // DbSet para la entidad Producto
        public DbSet<Producto> Productos { get; set; }

        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(x => x.Precio)
                .HasColumnType("decimal(18,2)");
        }
    }
}