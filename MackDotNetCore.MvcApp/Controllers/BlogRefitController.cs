using MackDotNetCore.MvcApp.Interfaces;
using MackDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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

		[ActionName("Create")]
		public async Task<IActionResult> BlogRefitCreate()
		{
			return View("BlogRefitCreate");
		}

		[HttpPost]
		[ActionName("Save")]
		public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
		{
			var model = await _blogApi.CreateBlogList(reqModel);
			return Redirect("/blogrefit");
		}

		[ActionName("Edit")]
		public async Task<IActionResult> BlogRefitEdit(int id)
		
		{
			var model = await _blogApi.Getbloglist(id);
			return View("BlogRefitEdit",model);
		}

		[HttpPost]
		[ActionName("Update")]
		public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
		{
			var model = await _blogApi.UpdateBlogList(id, reqModel);
			return Redirect("/blogrefit");
		}

		[ActionName("Delete")]
		public async Task<IActionResult> BlogDelete(int id)
		{
			var model = await _blogApi.DeleteBlog(id);
			return Redirect("/blogrefit");
		}
	}
}
