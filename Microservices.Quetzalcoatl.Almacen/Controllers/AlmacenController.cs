using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Quetzalcoatl.Almacen.Models;
using MicroServices.Quetzalcoatl.Almacenes.Data;
using Microservices.Quetzalcoatl.Almacen.DTOs.MicroServices.Quetzalcoatl.Almacenes.Dtos;


namespace MicroServices.Quetzalcoatl.Almacenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PermitirSoloCliente")]
    public class AlmacenController : ControllerBase
    {
        private readonly AlmacenDbContext _context;

        public AlmacenController(AlmacenDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/almacen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlmacenDto>>> GetAlmacenes(
            string? nombre = null, string? estatus = null)
        {
            var query = _context.Almacenes
                .Include(a => a.Sucursal)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(estatus))
                query = query.Where(a => a.Estatus == estatus);

            var almacenes = await query
                .Select(a => new AlmacenDto
                {
                    AlmacenId = a.AlmacenId,
                    Sucursal = a.Sucursal != null ? a.Sucursal.Nombre : "No asignado",
                    Nombre = a.Nombre,
                    Ubicacion = a.Ubicacion,
                    FechaAlta = a.FechaAlta,
                    FechaBaja = a.FechaBaja,
                    Estatus = a.Estatus
                    
                })
                .ToListAsync();

            return Ok(almacenes);
        }

        // 🔹 GET: api/almacen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlmacenDto>> GetAlmacen(int id)
        {
            var almacen = await _context.Almacenes
                .Include(a => a.Sucursal)
                .Where(a => a.AlmacenId == id)
                .Select(a => new AlmacenDto
                {
                    AlmacenId = a.AlmacenId,
                    Sucursal = a.Sucursal != null ? a.Sucursal.Nombre : "No asignado",
                    Nombre = a.Nombre,
                    Ubicacion = a.Ubicacion,
                    FechaAlta = a.FechaAlta,
                    FechaBaja = a.FechaBaja,
                    Estatus = a.Estatus,
                    
                })
                .FirstOrDefaultAsync();

            if (almacen == null)
                return NotFound(new { message = "Almacén no encontrado" });

            return Ok(almacen);
        }

        // 🔹 POST: api/almacen
        [HttpPost]
        public async Task<ActionResult<Almacen>> PostAlmacen(Almacen almacen)
        {
            if (string.IsNullOrEmpty(almacen.Nombre) || string.IsNullOrEmpty(almacen.Ubicacion))
                return BadRequest(new { message = "Nombre y Ubicación son obligatorios" });

            almacen.FechaAlta = DateTime.UtcNow;

            _context.Almacenes.Add(almacen);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlmacen), new { id = almacen.AlmacenId }, almacen);
        }

        // 🔹 PUT: api/almacen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlmacen(int id, [FromBody] Almacen almacen)
        {
            if (id != almacen.AlmacenId)
                return BadRequest(new { message = "El ID del almacén no coincide con el de la URL." });

            var almacenExistente = await _context.Almacenes.FindAsync(id);
            if (almacenExistente == null)
                return NotFound(new { message = "Almacén no encontrado." });

            // Solo actualizar los campos que realmente vienen en la petición
            almacenExistente.SucursalId = almacen.SucursalId ?? almacenExistente.SucursalId;
            almacenExistente.Nombre = !string.IsNullOrEmpty(almacen.Nombre) ? almacen.Nombre : almacenExistente.Nombre;
            almacenExistente.Ubicacion = !string.IsNullOrEmpty(almacen.Ubicacion) ? almacen.Ubicacion : almacenExistente.Ubicacion;
            almacenExistente.FechaAlta = almacen.FechaAlta != default ? almacen.FechaAlta : almacenExistente.FechaAlta;
            almacenExistente.FechaBaja = almacen.FechaBaja != default ? almacen.FechaBaja : almacenExistente.FechaBaja;
            almacenExistente.Estatus = !string.IsNullOrEmpty(almacen.Estatus) ? almacen.Estatus : almacenExistente.Estatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "Error al actualizar el almacén." });
            }

            return NoContent();
        }



        // 🔹 DELETE: api/almacen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlmacen(int id)
        {
            var almacen = await _context.Almacenes.FindAsync(id);
            if (almacen == null)
                return NotFound(new { message = "Almacén no encontrado" });

            _context.Almacenes.Remove(almacen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
