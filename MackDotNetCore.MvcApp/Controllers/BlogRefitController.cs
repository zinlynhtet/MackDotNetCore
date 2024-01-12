using MackDotNetCore.MvcApp.Interfaces;
using MackDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MackDotNetCore.MvcApp.Controllers
{
	public class BlogRefitController : Controller
	{
		private readonly IBlogApi _blogApi;

		public BlogRefitController(IBlogApi blogApi)
		{
			_blogApi = blogApi;
		}

		public async Task<IActionResult> Index()
		{
            BlogListResponseModel model = await _blogApi.GetBlogLists();
           
            return View(model);
		}
	}
}
