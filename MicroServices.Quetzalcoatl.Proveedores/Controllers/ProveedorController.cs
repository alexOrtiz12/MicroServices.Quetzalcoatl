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

     //  GET: api/proveedores
     [HttpGet]
     public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores(
         string? nombre = null, string? tipo = null, string? estatus = null)
     {
         IQueryable<Proveedor> query = _context.Proveedores.AsNoTracking(); // Evita modificaciones accidentales

         if (!string.IsNullOrEmpty(nombre))
             query = query.Where(p => EF.Functions.Like(p.Nombre, $"%{nombre}%")); // Evita SQL Injection

         if (!string.IsNullOrEmpty(tipo))
             query = query.Where(p => p.Tipo == tipo);

         if (!string.IsNullOrEmpty(estatus))
             query = query.Where(p => p.Estatus == estatus);

         return await query.ToListAsync();
     }

     //  GET: api/proveedores/5
     [HttpGet("{id:int}")]
     public async Task<ActionResult<Proveedor>> GetProveedor(int id)
     {
         var proveedor = await _context.Proveedores.AsNoTracking()
             .FirstOrDefaultAsync(p => p.ProveedorId == id);

         if (proveedor == null)
             return NotFound(new { message = "Proveedor no encontrado" });

         return proveedor;
     }

     //  POST: api/proveedores
     [HttpPost]
     public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
     {
         if (!ModelState.IsValid)
             return BadRequest(ModelState);

         proveedor.Sanitize(); //  Limpia datos antes de guardarlos
         proveedor.FechaAlta = DateTime.UtcNow;

         _context.Proveedores.Add(proveedor);
         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.ProveedorId }, proveedor);
     }

     // 🔹 PUT: api/proveedores/5
     [HttpPut("{id:int}")]
     public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
     {
         if (id != proveedor.ProveedorId)
             return BadRequest(new { message = "ID de proveedor no coincide" });

         if (!ModelState.IsValid)
             return BadRequest(ModelState);

         proveedor.Sanitize(); // ✅ Limpia datos antes de actualizar

         _context.Entry(proveedor).State = EntityState.Modified;

         try
         {
             await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
             if (!await _context.Proveedores.AnyAsync(e => e.ProveedorId == id))
                 return NotFound(new { message = "Proveedor no encontrado" });

             throw;
         }

         return NoContent();
     }

     // 🔹 DELETE: api/proveedores/5
     [HttpDelete("{id:int}")]
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

