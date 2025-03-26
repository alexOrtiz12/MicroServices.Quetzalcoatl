using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroServices.Quetzalcoatl.Proveedores.Data;
using MicroServices.Quetzalcoatl.Proveedores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServices.Quetzalcoatl.Proveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PermitirSoloCliente")]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorDbContext _context;

        public ProveedorController(ProveedorDbContext context)
        {
            _context = context;
        }

        // GET: api/proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores(
            string? nombre = null, string? tipo = null, string? estatus = null)
        {
            IQueryable<Proveedor> query = _context.Proveedores;

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(p => p.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(p => p.Tipo == tipo);

            if (!string.IsNullOrEmpty(estatus))
                query = query.Where(p => p.Estatus == estatus);

            return await query.ToListAsync();
        }

        // GET: api/proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound(new { message = "Proveedor no encontrado" });

            return proveedor;
        }

        // POST: api/proveedores
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            if (string.IsNullOrEmpty(proveedor.Nombre) || string.IsNullOrEmpty(proveedor.Email))
                return BadRequest(new { message = "Nombre y Email son obligatorios" });

            proveedor.FechaAlta = DateTime.UtcNow;

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.ProveedorId }, proveedor);
        }

        // PUT: api/proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.ProveedorId)
                return BadRequest(new { message = "ID de proveedor no coincide" });

            if (string.IsNullOrEmpty(proveedor.Nombre) || string.IsNullOrEmpty(proveedor.Email))
                return BadRequest(new { message = "Nombre y Email son obligatorios" });

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Proveedores.Any(e => e.ProveedorId == id))
                    return NotFound(new { message = "Proveedor no encontrado" });

                throw;
            }

            return NoContent();
        }

        // DELETE: api/proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound(new { message = "Proveedor no encontrado" });

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

