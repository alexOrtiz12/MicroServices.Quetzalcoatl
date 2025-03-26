using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroServices.Quetzalcoatl.ActivosFijos.Models
{
    public class Proveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorID { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}

