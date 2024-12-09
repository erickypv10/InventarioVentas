using InventarioVentas.Dto;
using InventarioVentas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using InventarioVentas.Data;
using AutoMapper;

namespace InventarioVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public ClientesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClienteDto clienteDto)
        {
            var cliente = mapper.Map<Cliente>(clienteDto);
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get(string nombre)
        {
            return await _context.cliente.Where(c => c.Nombre == nombre).ToListAsync();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _context.cliente.ToListAsync();
        }

        [HttpGet("direccion")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetDireccion(string direccion)
        {
            return await _context.cliente.Where(c => c.Direccion.Contains(direccion)).ToListAsync();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(int id, ClienteDto clienteDto)
        {
            var cliente = mapper.Map<Cliente>(clienteDto);
            cliente.Id = id;
            _context.Update(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}