using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Microservices.Quetzalcoatl.Almacen.Models
{
    public class Almacen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlmacenId { get; set; }
        public int? SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Estatus { get; set; } = string.Empty;

        public virtual Sucursal? Sucursal { get; set; }
    }
}