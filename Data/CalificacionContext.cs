using Microsoft.EntityFrameworkCore;
using MicroservicioDemo2.Models;

namespace MicroservicioDemo2.Data
{
    public class CalificacionContext : DbContext
    {
        public CalificacionContext(DbContextOptions<CalificacionContext> options)
            : base(options) { }

        public DbSet<Comentario> Comentarios { get; set; }
    }
}
