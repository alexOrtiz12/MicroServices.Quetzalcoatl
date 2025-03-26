using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroServices.Quetzalcoatl.ActivosFijos.Models
{
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SucursalID { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}

