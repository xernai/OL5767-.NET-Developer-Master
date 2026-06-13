using Microsoft.EntityFrameworkCore;
using NikiShop.Ecommerce.WebMvc.Models;

namespace NikiShop.Ecommerce.WebMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
    }
}
