using Microsoft.EntityFrameworkCore;
using MackDotNetCore.MinimalApi.Model;

namespace MackDotNetCore.MinimalApi
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<BlogDataModel> Blogs { get; set; }
	}
}
