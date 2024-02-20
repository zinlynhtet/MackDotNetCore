﻿using MackDotNetCore.MvcApp.EFDbContext;
using MackDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AEHKLMNSTZDotNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDBContext _context;

        public BlogAjaxController(AppDBContext context)
        {
            _context = context;
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

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }


        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.blog_id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blogajax/list");
            }
            return View("BlogEdit", blog);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.blog_id == id);

            if (blog != null)
            {
                blog.blog_title = reqModel.blog_title;
                blog.blog_authour = reqModel.blog_authour;
                blog.blog_content = reqModel.blog_content;

                _context.Blogs.Update(blog);
                var result = await _context.SaveChangesAsync();

                string message = result > 0 ? "Update Successful." : "Update Failed.";
                TempData["Message"] = message;
                TempData["IsSuccess"] = result > 0;

                MessageModel model = new MessageModel(result > 0, message);
                return Json(model);
            }

            return Json(new MessageModel(false, "No Data Found to Update"));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(BlogDataModel reqModel)
        {
            BlogDataModel? blog = await _context.Blogs.FirstOrDefaultAsync(x => x.blog_id == reqModel.blog_id);

            if (blog is null)
            {
                return Json(new MessageModel(false, "No data found."));
            }

            _context.Blogs.Remove(blog);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Your blog has been deleted." : "Deleting Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }
    }
}
