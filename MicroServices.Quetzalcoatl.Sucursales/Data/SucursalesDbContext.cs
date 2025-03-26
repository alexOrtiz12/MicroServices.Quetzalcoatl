using MicroServices.Quetzalcoatl.Sucursales.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MicroServices.Quetzalcoatl.Sucursales.Data
{
    public class SucursalesDbContext : DbContext
    {
        public SucursalesDbContext(DbContextOptions<SucursalesDbContext> options)
            : base(options) { }

        public DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sucursal>()
                .HasKey(s => s.SucursalId); // Asegura que la clave primaria esté bien definida

            base.OnModelCreating(modelBuilder);
        }
    }
}
