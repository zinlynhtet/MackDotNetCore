using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MackDotNetCore.MinimalApi.Model;
using MackDotNetCore.MinimalApi;

public static class BlogService
{
	public static void AddBlogService(this IEndpointRouteBuilder app)
	{
		app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
		{
			var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.blog_id == id);

			if (blog == null)
			{
				return Results.NotFound(new BlogResponseModel
				{
					IsSuccess = false,
					Message = $"Blog with ID {id} not found"
				});
			}

			return Results.Ok(new BlogResponseModel
			{
				IsSuccess = true,
				Message = "Blog retrieved successfully",
				Data = blog
			});
		})
		.WithName("GetBlog")
		.WithOpenApi();

		app.MapPost("/blog", async ([FromServices] AppDbContext db, BlogDataModel blog) =>
		{
			await db.Blogs.AddAsync(blog);
			await db.SaveChangesAsync();

			return Results.Created($"/blog/{blog.blog_id}", new BlogResponseModel
			{
				IsSuccess = true,
				Message = "Blog created successfully",
				Data = blog
			});
		})
		.WithName("CreateBlog")
		.WithOpenApi();

		app.MapPut("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel updatedBlog) =>
		{
			var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.blog_id == id);

			if (blog == null)
			{
				return Results.NotFound(new BlogResponseModel
				{
					IsSuccess = false,
					Message = $"Blog with ID {id} not found"
				});
			}

			blog.blog_title = updatedBlog.blog_title;
			blog.blog_authour = updatedBlog.blog_authour;
			blog.blog_content = updatedBlog.blog_content;

			await db.SaveChangesAsync();

			return Results.Ok(new BlogResponseModel
			{
				IsSuccess = true,
				Message = "Blog updated successfully",
				Data = blog
			});
		})
		.WithName("UpdateBlog")
		.WithOpenApi();

		app.MapDelete("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
		{
			var blog = await db.Blogs.FirstOrDefaultAsync(b => b.blog_id == id);

			if (blog == null)
			{
				return Results.NotFound(new BlogResponseModel
				{
					IsSuccess = false,
					Message = $"Blog with ID {id} not found"
				});
			}

			db.Blogs.Remove(blog);
			await db.SaveChangesAsync();

			return Results.Ok(new BlogResponseModel
			{
				IsSuccess = true,
				Message = "Blog deleted successfully",
				Data = blog
			});
		})
		.WithName("DeleteBlog")
		.WithOpenApi();
	}
}
