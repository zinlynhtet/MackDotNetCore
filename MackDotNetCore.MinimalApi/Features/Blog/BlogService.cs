using Microsoft.AspNetCore.Mvc;
using MackDotNetCore.MinimalApi;
using Microsoft.EntityFrameworkCore;
using MackDotNetCore.MinimalApi.Model;

public static class BlogService
{
	public static void AddBlogService(this IEndpointRouteBuilder app)
	{
		app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
		{
			var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.blog_id == id);
			return Results.Ok(blog);
		})
		.WithName("GetBlog")
		.WithOpenApi();

		app.MapPost("/blog", async ([FromServices] AppDbContext db, BlogDataModel blog) =>
		{
			await db.Blogs.AddAsync(blog);
			await db.SaveChangesAsync();
			return Results.Created($"/blog/{blog.blog_id}", blog);
		})
		.WithName("CreateBlog")
		.WithOpenApi();

	}
}