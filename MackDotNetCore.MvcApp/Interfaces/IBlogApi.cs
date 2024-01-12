using MackDotNetCore.MvcApp.Models;
using Refit;

namespace MackDotNetCore.MvcApp.Interfaces
{
	public interface IBlogApi
	{
		[Get("/api/blog")]
		Task<BlogListResponseModel> GetBlogLists();

		[Get("/api/blog/{id}")]
		Task<BlogResponseModel> Getbloglist(int id);

		[Post("/api/blog")]
		Task<BlogResponseModel> CreateBlogList(BlogDataModel blog);

		[Put("/api/blog/{id}")]
		Task<BlogResponseModel> UpdateBlogList(int id, BlogDataModel blog);

		[Delete("/api/blog/{id}")]
		Task<BlogResponseModel> DeleteBlog(int id);
	}
}
