using Microsoft.EntityFrameworkCore;
using MicroServices.Quetzalcoatl.ActivosFijos.Models;

namespace MicroServices.Quetzalcoatl.ActivosFijos.Data
{
    public class ActivoFijoDbContext : DbContext
    {
        public ActivoFijoDbContext(DbContextOptions<ActivoFijoDbContext> options) : base(options) { }

        public DbSet<ActivoFijo> ActivosFijos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivoFijo>()
                .HasOne(a => a.Proveedor)
                .WithMany()
                .HasForeignKey(a => a.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActivoFijo>()
                .HasOne(a => a.Sucursal)
                .WithMany()
                .HasForeignKey(a => a.SucursalID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
