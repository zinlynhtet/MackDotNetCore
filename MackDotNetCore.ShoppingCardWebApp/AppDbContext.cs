using MackDotNetCore.ShoppingCardWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MackDotNetCore.ShoppingCardWebApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ItemDataModel> Data { get; set; }
    }
}
