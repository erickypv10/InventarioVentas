using InventarioVentas.Dto;
using InventarioVentas.Dtos; // Asegúrate de que la ruta sea correcta
using InventarioVentas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using InventarioVentas.Data;
using Microsoft.EntityFrameworkCore;

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



        // GET: api/clientes/{clienteId}/ventas
        [HttpGet("clientes/{clienteId}/ventas")]
        public async Task<ActionResult<IEnumerable<VentaDto>>> GetVentasPorCliente(int clienteId)
        {
            var ventas = await _context.venta
                .Where(v => v.ClienteId == clienteId)
                .Select(v => new VentaDto
                {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    Total = v.Total,
                    ClienteId = v.ClienteId,
                    VentaProductos = v.VentaProductos.Select(vp => new VentaProductoDto
                    {
                        ProductoId = vp.ProductoId,
                        Cantidad = vp.Cantidad
                    }).ToList()
                }).ToListAsync();

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




        [HttpPost]
        public async Task<ActionResult<VentaDto>> PostVenta(VentaDto ventaDto)
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Total = ventaDto.Total,
                ClienteId = ventaDto.ClienteId,
                VentaProductos = ventaDto.VentaProductos.Select(vp => new VentaProducto
                {
                    ProductoId = vp.ProductoId,
                    Cantidad = vp.Cantidad
                }).ToList()
            };

            _context.venta.Add(venta);

            // Actualizar el stock de los productos vendidos
            foreach (var vp in venta.VentaProductos)
            {
                var producto = await _context.productos.FindAsync(vp.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= vp.Cantidad;
                    _context.Entry(producto).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

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
