namespace MicroServices.Quetzalcoatl.Proveedores.Models
{
    public class Proveedor
    {
       [Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int ProveedorId { get; set; }

[Required, MaxLength(100)]
public string Nombre { get; set; } = string.Empty;

[Required, MaxLength(50)]
public string Tipo { get; set; } = string.Empty;

[Required, MaxLength(200)]
public string Direccion { get; set; } = string.Empty;

[Required, MaxLength(15)]
public string Telefono { get; set; } = string.Empty;

[Required, EmailAddress, MaxLength(100)]
public string Email { get; set; } = string.Empty;

public DateTime FechaAlta { get; set; }
public DateTime? FechaBaja { get; set; }

[Required, MaxLength(20)]
public string Estatus { get; set; } = string.Empty;

// 🔹 Método para sanitizar entradas
public void Sanitize()
{
    Nombre = HttpUtility.HtmlEncode(Nombre);
    Tipo = HttpUtility.HtmlEncode(Tipo);
    Direccion = HttpUtility.HtmlEncode(Direccion);
    Telefono = HttpUtility.HtmlEncode(Telefono);
    Email = HttpUtility.HtmlEncode(Email);
    Estatus = HttpUtility.HtmlEncode(Estatus);
}
    }
}
