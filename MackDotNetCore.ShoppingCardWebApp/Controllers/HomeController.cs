using MackDotNetCore.ShoppingCardWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MackDotNetCore.ShoppingCardWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        // Use a single constructor that takes both dependencies
        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [ActionName("Index")]
        public IActionResult Index()
        {
            List<ItemDataModel> lst = _context.Data.ToList();
            ItemDataResponseModel responseModel = new ItemDataResponseModel
            {
                Data = lst
            };
            return View("Index", responseModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
