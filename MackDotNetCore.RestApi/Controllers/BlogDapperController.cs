using Dapper;
using MackDotNetCore.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace MackDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlconnectionStringBuilder;
        public BlogDapperController()
        {
            sqlconnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "MSI\\MSSQLSEVER", // sever name
                InitialCatalog = "ALTDotNetCore", //database name
                UserID = "sa",
                Password = "Queen@2001"

            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            BlogListResponseModel model = new BlogListResponseModel();
            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = lst;
            return Ok(model);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_blog where blog_id = @blog_id";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { blog_id = id }).FirstOrDefault();
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
            string query = @"INSERT INTO [dbo].[Tbl_blog]
           ([blog_title]
           ,[blog_authour]
           ,[blog_content])
     VALUES
           (@blog_title,
		   @blog_authour,
		   @blog_content)";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
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
            string query = "select * from tbl_blog where blog_id = @blog_id";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { blog_id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            query = @"UPDATE [dbo].[Tbl_blog]
   SET [blog_title] = @blog_title
      ,[blog_authour] = @blog_authour
      ,[blog_content] = @blog_content
 WHERE blog_Id = @blog_id";

            using IDbConnection db2 = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, blog);
            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful" : "Saving Failed.",
                Data = item

            };
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            string query = "select * from tbl_blog where blog_id = @blog_id";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { blog_id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            query = @"UPDATE [dbo].[Tbl_blog]
   SET [blog_title] = @blog_title
      ,[blog_authour] = @blog_authour
      ,[blog_content] = @blog_content
 WHERE blog_Id = @blog_id";
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.blog_title))
            {
                query += " [blog_title] =@blog_title,";
                item.blog_title = blog.blog_title;
            }
            if (!string.IsNullOrEmpty(blog.blog_authour))
            {
                query += " [blog_authour] =@blog_authour,";
                item.blog_authour = blog.blog_authour;
            }
            if (!string.IsNullOrEmpty(blog.blog_content))
            {
                query += " [blog_content] =@blog_content,";
                item.blog_content = blog.blog_content;
            }
            if (conditions.Length == 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            query = $@"UPDATE [dbo].[Tbl_blog]
                    SET{conditions}
                    WHERE blog_Id = @blog_id";

            using IDbConnection db2 = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, blog);

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
            string query = "select * from tbl_blog where blog_id = @blog_id";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { blog_id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            query = @"DELETE FROM [dbo].[Tbl_blog]
                           WHERE blog_Id = @blog_id";
            using IDbConnection db2 = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, new BlogDataModel { blog_id = id });
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


