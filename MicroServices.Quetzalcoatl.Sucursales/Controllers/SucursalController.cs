using MicroServices.Quetzalcoatl.Sucursales.Data;
using MicroServices.Quetzalcoatl.Sucursales.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace MicroServices.Quetzalcoatl.Sucursales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class SucursalController : ControllerBase
    {
        private readonly SucursalesDbContext _context;

        public SucursalController(SucursalesDbContext context)
        {
            _context = context;
        }

        // GET: api/sucursales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales(
            string? ciudad = null, string? estado = null, string? pais = null)
        {
            IQueryable<Sucursal> query = _context.Sucursales;

            if (!string.IsNullOrEmpty(ciudad))
                query = query.Where(s => s.Ciudad == ciudad);

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(s => s.Estado == estado);

            if (!string.IsNullOrEmpty(pais))
                query = query.Where(s => s.Pais == pais);

            return await query.ToListAsync();
        }

        // GET: api/sucursales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
                return NotFound(new { message = "Sucursal no encontrada" });

            return sucursal;
        }

        // POST: api/sucursales
        [HttpPost]
        public async Task<ActionResult<Sucursal>> PostSucursal(Sucursal sucursal)
        {
            if (string.IsNullOrEmpty(sucursal.Nombre) || string.IsNullOrEmpty(sucursal.Direccion))
                return BadRequest(new { message = "Nombre y Dirección son obligatorios" });

            sucursal.FechaAlta = DateTime.UtcNow; // Asigna la fecha actual

            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.SucursalId }, sucursal);
        }

        // PUT: api/sucursales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSucursal(int id, Sucursal sucursal)
        {
            if (id != sucursal.SucursalId)
                return BadRequest(new { message = "ID de sucursal no coincide" });

            if (string.IsNullOrEmpty(sucursal.Nombre) || string.IsNullOrEmpty(sucursal.Direccion))
                return BadRequest(new { message = "Nombre y Dirección son obligatorios" });

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sucursales.Any(e => e.SucursalId == id))
                    return NotFound(new { message = "Sucursal no encontrada" });

                throw;
            }

            return NoContent();
        }

        // DELETE: api/sucursales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
                return NotFound(new { message = "Sucursal no encontrada" });

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
