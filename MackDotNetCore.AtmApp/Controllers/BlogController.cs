using MackDotNetCore.AtmApp.EFDbContext;
using MackDotNetCore.AtmApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MackDotNetCore.AtmApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        //get
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
			List<BlogDataModel> lst = _context.Blogs.ToList();  
            return View("BlogIndex", lst);
        }
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }
        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogDataModel reqModel)
        {
            _context.Blogs.Add(reqModel);
            _context.SaveChanges();
            return Redirect("/blog");
        }
    }
}
