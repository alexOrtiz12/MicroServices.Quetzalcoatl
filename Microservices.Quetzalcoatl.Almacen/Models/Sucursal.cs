using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Microservices.Quetzalcoatl.Almacen.Models
{
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SucursalID { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
