using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroServices.Quetzalcoatl.ActivosFijos.Models
{
    public class ActivoFijo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivoFijoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; }
        public int ProveedorID { get; set; }
        public int? SucursalID { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Estatus { get; set; } = string.Empty;
            
        // Agregar propiedades de navegación
        public virtual Proveedor? Proveedor { get; set; } // Relación con Proveedor
        public virtual Sucursal? Sucursal { get; set; }   // Relación con Sucursal
    }
}
