using MackDotNetCore.SessionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MackDotNetCore.AtmApp.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> IndexAsync(LoginViewModel reqModel)
		{
			
		

			

			HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(reqModel));
			return Redirect("/");
		}
	}
}
