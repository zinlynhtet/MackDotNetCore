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
	}
}
