using InventarioVentas.Dto;
using InventarioVentas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using InventarioVentas.Data;

namespace InventarioVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public ActionResult<IEnumerable<ClienteDto>> GetClientes()
        {
            var clientes = _context.cliente.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                CorreoElectronico = c.CorreoElectronico,
                Telefono = c.Telefono,
                Direccion = c.Direccion
            }).ToList();

            return Ok(clientes);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public ActionResult<ClienteDto> GetCliente(int id)
        {
            var cliente = _context.cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDto = new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                CorreoElectronico = cliente.CorreoElectronico,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion
            };

            return Ok(clienteDto);
        }

        // POST: api/clientes
        [HttpPost]
        public ActionResult<ClienteDto> PostCliente(ClienteDto clienteDto)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDto.Nombre,
                CorreoElectronico = clienteDto.CorreoElectronico,
                Telefono = clienteDto.Telefono,
                Direccion = clienteDto.Direccion
            };

            _context.cliente.Add(cliente);
            _context.SaveChanges();

            clienteDto.Id = cliente.Id;

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, clienteDto);
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        public IActionResult PutCliente(int id, ClienteDto clienteDto)
        {
            if (id != clienteDto.Id)
            {
                return BadRequest();
            }

            var cliente = _context.cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            // Actualizar el modelo usando los datos del DTO
            cliente.Nombre = clienteDto.Nombre;
            cliente.CorreoElectronico = clienteDto.CorreoElectronico;
            cliente.Telefono = clienteDto.Telefono;
            cliente.Direccion = clienteDto.Direccion;

            _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _context.cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.cliente.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }
    }
}