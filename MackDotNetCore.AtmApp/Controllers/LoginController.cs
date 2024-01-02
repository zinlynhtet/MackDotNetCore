using MackDotNetCore.AtmApp.EFDbContext;
using MackDotNetCore.AtmApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MackDotNetCore.AtmApp.Controllers
{
	public class LoginController : Controller
	{
		private readonly AppDbContext _context;

		public LoginController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> IndexAsync(BlogDataModel reqModel)
		{
			
		

			var blog = await _context.Blogs
				.FirstOrDefaultAsync(b => b.CardNum == reqModel.CardNum && b.Pin == reqModel.Pin);

			if (blog == null)
			{
				return RedirectToAction("Index");
			}

			HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(reqModel));
			return Redirect("/");
		}
	}
}
