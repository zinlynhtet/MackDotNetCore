using Microsoft.AspNetCore.Mvc;
using MackDotNetCore.MinimalApi;
using Microsoft.EntityFrameworkCore;

public static class BlogService
{
	public static void AddBlogService(this IEndpointRouteBuilder app)
	{
		app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
		{
			var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.blog_id == id);
			return Results.Ok(blog);
		})
		.WithName("GetBlogs")
		.WithOpenApi();
	}
}