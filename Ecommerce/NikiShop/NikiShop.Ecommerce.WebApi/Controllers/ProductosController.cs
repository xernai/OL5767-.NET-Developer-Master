using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikiShop.Ecommerce.WebApi.Data;
using NikiShop.Ecommerce.WebApi.Models;

namespace NikiShop.Ecommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos?page=1&pageSize=20
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            return await _context.Productos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // GET: api/Productos/search?name=xyz
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Producto>>> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("The 'name' parameter is required.");
            }

            var productos = await _context.Productos
                .Where(p => p.Nombre.Contains(name))
                .ToListAsync();

            return productos;
        }

        // GET: api/Productos/saludar?nombre=Juan
        [HttpGet("saludar")]
        public string GetSalute([FromQuery] string nombre)
        {
            return "Hola" + nombre;
        }
    }
}
