using MackDotNetCore.ConsoleApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.RefitExamples
{
	public interface IBlogApi
	{
		[Get("/api/blog")]
		Task<BlogListResponseModel> GetBlogLists();

		[Get("/api/blog/{id}")]
		Task<BlogDataModel> GetBlogList(int id);

		[Post("/api/blog")]
		Task<BlogResponseModel> CreateBlogList(BlogDataModel blog);

		[Put("/api/blog/{id}")]
		Task<BlogResponseModel> UpdateBlogList(BlogDataModel blog);


		[Delete("/api/blog/{id}")]
		Task<BlogResponseModel> DeleteBlogList(int id);
	}
}
