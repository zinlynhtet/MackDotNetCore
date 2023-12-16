using MackDotNetCore.MvcApp.EFDbContext;
using MackDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MackDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDBContext _context;

        public BlogController(AppDBContext context)
        {
            _context = context;
        }

        // Get / List
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            List<BlogDataModel> lst = _context.Blogs.ToList();
            //ViewData["Title"] = "dfasdfsafsd";
            return View("BlogIndex", lst);
            //return Redirect("/blog/create");
        }
        [ActionName("List")]
        public async Task<IActionResult> BlogList(int pageNo = 1, int pageSize = 10)
        {
            BlogDataResponseModel model = new BlogDataResponseModel();
            List<BlogDataModel> lst = _context.Blogs.AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int rowCount = await _context.Blogs.CountAsync();
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
                pageCount++;

            model.Blogs = lst;
            //model.PageSetting = new PageSettingModel
            //{
            //    PageCount = pageCount,
            //    PageNo = pageNo,
            //    PageSize = pageSize
            //};
            model.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/blog/list");

            return View("BlogList", model);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            await _context.Blogs.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.blog_id == id))
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.blog_id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            return View("BlogEdit", blog);
        }


        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.blog_id == id))
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.blog_id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            blog.blog_title = reqModel.blog_title;
            blog.blog_authour = reqModel.blog_authour;
            blog.blog_content = reqModel.blog_content;

            int result = _context.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.blog_id == id))
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.blog_id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            _context.Remove(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }
    }
}
