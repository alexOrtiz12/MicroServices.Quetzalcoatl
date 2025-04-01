using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MicroServices.Quetzalcoatl.Sucursales.Models
{
    public class Sucursal
    {
        [Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int SucursalId { get; set; }

[Required, MaxLength(100)]
public string Nombre { get; set; } = string.Empty;

[Required, MaxLength(200)]
public string Direccion { get; set; } = string.Empty;

[Required, MaxLength(100)]
public string Ciudad { get; set; } = string.Empty;

[Required, MaxLength(100)]
public string Estado { get; set; } = string.Empty;

[Required, MaxLength(100)]
public string Pais { get; set; } = string.Empty;

[Required, MaxLength(10)]
public string CodigoPostal { get; set; } = string.Empty;

public decimal Latitud { get; set; }
public decimal Longitud { get; set; }

public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
public DateTime? FechaBaja { get; set; }

[Required, MaxLength(20)]
public string Estatus { get; set; } = string.Empty;

// Método para sanitizar entradas
public void Sanitize()
{
    Nombre = HttpUtility.HtmlEncode(Nombre);
    Direccion = HttpUtility.HtmlEncode(Direccion);
    Ciudad = HttpUtility.HtmlEncode(Ciudad);
    Estado = HttpUtility.HtmlEncode(Estado);
    Pais = HttpUtility.HtmlEncode(Pais);
    CodigoPostal = HttpUtility.HtmlEncode(CodigoPostal);
    Estatus = HttpUtility.HtmlEncode(Estatus);
}
    }
}
