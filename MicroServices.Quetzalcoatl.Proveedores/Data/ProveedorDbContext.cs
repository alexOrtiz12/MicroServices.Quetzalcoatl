using MicroServices.Quetzalcoatl.Proveedores.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServices.Quetzalcoatl.Proveedores.Data
{
    public class ProveedorDbContext : DbContext
    {
        public ProveedorDbContext(DbContextOptions<ProveedorDbContext> options)
            : base(options) { }

        public DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proveedor>()
                .HasKey(p => p.ProveedorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
