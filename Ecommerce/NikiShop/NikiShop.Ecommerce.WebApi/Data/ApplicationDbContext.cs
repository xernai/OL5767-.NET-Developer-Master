using Microsoft.EntityFrameworkCore;
using NikiShop.Ecommerce.WebApi.Models;

namespace NikiShop.Ecommerce.WebApi.Data
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
