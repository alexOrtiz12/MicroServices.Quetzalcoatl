namespace Microservices.Quetzalcoatl.Almacen.DTOs
{
    namespace MicroServices.Quetzalcoatl.Almacenes.Dtos
    {
        public class AlmacenDto
        {
            public int AlmacenId { get; set; }
            public string Sucursal { get; set; } = "No asignado";
            public string Nombre { get; set; } = string.Empty;
            public string Ubicacion { get; set; } = string.Empty;
            public DateTime FechaAlta { get; set; }
            public DateTime? FechaBaja { get; set; }
            public string Estatus { get; set; } = string.Empty;
            
        }
    }

}
