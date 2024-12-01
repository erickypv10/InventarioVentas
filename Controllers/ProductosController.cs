using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioVentas.Data;
using InventarioVentas.Models;
using InventarioVentas.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace InventarioVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Getproductos()
        {
            var productos = _context.productos.Select(p => new ProductoDto{
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock
            }).ToList();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _context.productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }
            var productoDto = new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
            return Ok(productoDto);
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        public IActionResult PutProducto(int id, ProductoDto productoDto)
        {
            if (id != productoDto.Id)
            {
                return BadRequest();
            }

            var producto = _context.productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Actualizar el modelo usando los datos del DTO
            producto.Nombre = productoDto.Nombre;
            producto.Precio = productoDto.Precio;
            producto.Stock = productoDto.Stock;

            _context.Entry(producto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> PostProducto(ProductoDto productoDto)
        {
            var producto = new Producto
            {
                Id = productoDto.Id,
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            };
            _context.productos.Add(producto);
           await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, productoDto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.productos.Any(e => e.Id == id);
        }
    }
}
