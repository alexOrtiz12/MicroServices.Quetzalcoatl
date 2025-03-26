using Microsoft.EntityFrameworkCore;
using Microservices.Quetzalcoatl.Almacen.Models;

namespace MicroServices.Quetzalcoatl.Almacenes.Data
{
    public class AlmacenDbContext : DbContext
    {
        public AlmacenDbContext(DbContextOptions<AlmacenDbContext> options) : base(options) { }

        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Almacen>().ToTable("Almacen");

            modelBuilder.Entity<Almacen>()
                .HasOne(a => a.Sucursal)
                .WithMany()
                .HasForeignKey(a => a.SucursalId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
