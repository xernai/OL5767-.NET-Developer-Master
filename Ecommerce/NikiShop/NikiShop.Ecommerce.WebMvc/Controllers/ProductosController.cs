using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikiShop.Ecommerce.WebMvc.Data;
using System.Linq;

namespace NikiShop.Ecommerce.WebMvc.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _context.Productos.Take(20).ToListAsync();
            return View(productos);
        }
    }
}
