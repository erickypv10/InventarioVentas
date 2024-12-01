using InventarioVentas.Dto;
using InventarioVentas.Dtos; // Asegúrate de que la ruta sea correcta
using InventarioVentas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using InventarioVentas.Data;

namespace InventarioVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ventas
        [HttpGet]
        public ActionResult<IEnumerable<VentaDto>> GetVentas()
        {
            var ventas = _context.venta.Select(v => new VentaDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                ClienteId = v.ClienteId
            }).ToList();

            return Ok(ventas);
        }

        // GET: api/ventas/{id}
        [HttpGet("{id}")]
        public ActionResult<VentaDto> GetVenta(int id)
        {
            var venta = _context.venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            var ventaDto = new VentaDto
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                ClienteId = venta.ClienteId
            };

            return Ok(ventaDto);
        }

        // POST: api/ventas
        [HttpPost]
        public ActionResult<VentaDto> PostVenta(VentaDto ventaDto)
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now, // O la fecha que desees usar
                Total = ventaDto.Total, // Asigna el total desde el DTO
                ClienteId = ventaDto.ClienteId // Asigna el cliente desde el DTO
            };

            _context.venta.Add(venta);
            _context.SaveChanges();

            ventaDto.Id = venta.Id; // Asigna el ID generado

            return CreatedAtAction(nameof(GetVenta), new { id = venta.Id }, ventaDto);
        }

        // PUT: api/ventas/{id}
        [HttpPut("{id}")]
        public IActionResult PutVenta(int id, VentaDto ventaDto)
        {
            if (id != ventaDto.Id)
            {
                return BadRequest();
            }

            var venta = _context.venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            // Actualiza los campos del modelo usando los datos del DTO
            venta.Fecha = ventaDto.Fecha;
            venta.Total = ventaDto.Total;
            venta.ClienteId = ventaDto.ClienteId;

            _context.Entry(venta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/ventas/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteVenta(int id)
        {
            var venta = _context.venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.venta.Remove(venta);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
