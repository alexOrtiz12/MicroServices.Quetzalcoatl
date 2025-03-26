using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroServices.Quetzalcoatl.ActivosFijos.Data;
using MicroServices.Quetzalcoatl.ActivosFijos.Models;
using MicroServices.Quetzalcoatl.ActivosFijos.DTOs; // Importamos el DTO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MicroServices.Quetzalcoatl.ActivosFijos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PermitirSoloCliente")]
    public class ActivoFijoController : ControllerBase
    {
        private readonly ActivoFijoDbContext _context;

        public ActivoFijoController(ActivoFijoDbContext context)
        {
            _context = context;
        }

        // GET: api/activosfijos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivoFijoDto>>> GetActivosFijos(
            string? nombre = null, string? serial = null, string? estatus = null)
        {
            var query = _context.ActivosFijos
                .Include(a => a.Proveedor)
                .Include(a => a.Sucursal)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(serial))
                query = query.Where(a => a.Serial == serial);

            if (!string.IsNullOrEmpty(estatus))
                query = query.Where(a => a.Estatus == estatus);

            var activos = await query.Select(a => new ActivoFijoDto
            {
                ActivoFijoId = a.ActivoFijoId,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                Serial = a.Serial,
                FechaCompra = a.FechaCompra,
                Proveedor = a.Proveedor != null ? a.Proveedor.Nombre : "No asignado",
                Sucursal = a.Sucursal != null ? a.Sucursal.Nombre : "No asignado",
                FechaAlta = a.FechaAlta,
                FechaBaja = a.FechaBaja,
                Estatus = a.Estatus
            }).ToListAsync();

            return Ok(activos);
        }

        // GET: api/activosfijos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivoFijoDto>> GetActivoFijo(int id)
        {
            var activo = await _context.ActivosFijos
                .Include(a => a.Proveedor)
                .Include(a => a.Sucursal)
                .Where(a => a.ActivoFijoId == id)
                .Select(a => new ActivoFijoDto
                {
                    ActivoFijoId = a.ActivoFijoId,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    Serial = a.Serial,
                    FechaCompra = a.FechaCompra,
                    Proveedor = a.Proveedor != null ? a.Proveedor.Nombre : "No asignado",
                    Sucursal = a.Sucursal != null ? a.Sucursal.Nombre : "No asignado",
                    FechaAlta = a.FechaAlta,
                    FechaBaja = a.FechaBaja,
                    Estatus = a.Estatus
                })
                .FirstOrDefaultAsync();

            if (activo == null)
                return NotFound(new { message = "Activo fijo no encontrado" });

            return Ok(activo);
        }

        // POST: api/activosfijos
        [HttpPost]
        public async Task<ActionResult<ActivoFijo>> PostActivoFijo(ActivoFijo activoFijo)
        {
            if (string.IsNullOrEmpty(activoFijo.Nombre) || string.IsNullOrEmpty(activoFijo.Serial))
                return BadRequest(new { message = "Nombre y Serial son obligatorios" });

            activoFijo.FechaAlta = DateTime.UtcNow;

            _context.ActivosFijos.Add(activoFijo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivoFijo), new { id = activoFijo.ActivoFijoId }, activoFijo);
        }

        // PUT: api/activosfijos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivoFijo(int id, ActivoFijo activoFijo)
        {
            if (id != activoFijo.ActivoFijoId)
                return BadRequest(new { message = "ID de activo fijo no coincide" });

            if (string.IsNullOrEmpty(activoFijo.Nombre) || string.IsNullOrEmpty(activoFijo.Serial))
                return BadRequest(new { message = "Nombre y Serial son obligatorios" });

            _context.Entry(activoFijo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ActivosFijos.Any(e => e.ActivoFijoId == id))
                    return NotFound(new { message = "Activo fijo no encontrado" });

                throw;
            }

            return NoContent();
        }

        // DELETE: api/activosfijos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivoFijo(int id)
        {
            var activoFijo = await _context.ActivosFijos.FindAsync(id);
            if (activoFijo == null)
                return NotFound(new { message = "Activo fijo no encontrado" });

            _context.ActivosFijos.Remove(activoFijo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
