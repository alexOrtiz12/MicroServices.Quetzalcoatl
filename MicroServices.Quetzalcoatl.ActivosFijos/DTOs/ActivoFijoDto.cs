namespace MicroServices.Quetzalcoatl.ActivosFijos.DTOs
{
    public class ActivoFijoDto
    {
        public int ActivoFijoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public string? Sucursal { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Estatus { get; set; } = string.Empty;
    }
}
