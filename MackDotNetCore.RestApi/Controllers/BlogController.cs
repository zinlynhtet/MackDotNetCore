using MackDotNetCore.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MackDotNetCore.RestApi.Controllers
{
    //api/Blog/
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                AppDBContext dbContext = new AppDBContext();
                List<BlogDataModel> lst = dbContext.Blogs.ToList(); 
                BlogListResponseModel model = new BlogListResponseModel
                {
                    IsSuccess = true,
                    Message = "Success",
                    Data = lst
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(new BlogListResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message// return summary message
                   /* Message = ex.ToString()*/,// return detail error
                });

            }
            //    AppDBContext dbContext = new AppDBContext();
            //    List<BlogDataModel> lst = dbContext.Blogs.ToList();
            //    BlogListResponseModel model = new BlogListResponseModel();
            //    model.IsSuccess = true;
            //            model.Message = "Success";
            //            model.Data = lst;
            //            return Ok(model);
            //}
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful" : "Saving Failed.",
                Data = blog
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            item.blog_title = blog.blog_title;
            item.blog_authour = blog.blog_authour;
            item.blog_content = blog.blog_content;
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful" : "Saving Failed.",
                Data = blog

            };
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            if (!string.IsNullOrEmpty(blog.blog_title))
            {
                item.blog_title = blog.blog_title;
            }
            if (!string.IsNullOrEmpty(blog.blog_authour))
            {
                item.blog_authour = blog.blog_authour;
            }
            if (!string.IsNullOrEmpty(blog.blog_content))
            {
                item.blog_content = blog.blog_content;
            }
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful" : " Updating failed.",
                Data = item
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful" : " Deleting failed.",
                Data = item
            };

            return Ok(model);
        }

    }
}
