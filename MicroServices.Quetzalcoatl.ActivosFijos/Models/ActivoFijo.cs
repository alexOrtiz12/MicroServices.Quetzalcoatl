using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace MicroServices.Quetzalcoatl.ActivosFijos.Models
{
    public class ActivoFijo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivoFijoId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Serial { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCompra { get; set; }

        [Required]
        public int ProveedorID { get; set; }

        public int? SucursalID { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        public DateTime? FechaBaja { get; set; }

        [Required, MaxLength(20)]
        public string Estatus { get; set; } = string.Empty;

        // Propiedades de navegación
        public virtual Proveedor? Proveedor { get; set; }
        public virtual Sucursal? Sucursal { get; set; }

        // 🔹 Método para sanitizar entradas y prevenir XSS
        public void Sanitize()
        {
            Nombre = HttpUtility.HtmlEncode(Nombre);
            Descripcion = HttpUtility.HtmlEncode(Descripcion);
            Serial = HttpUtility.HtmlEncode(Serial);
            Estatus = HttpUtility.HtmlEncode(Estatus);
        }
    }
}
