using Azure;
using Dapper;
using MackDotNetCore.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MackDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlconnectionStringBuilder;
        public BlogAdoController()
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
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();
            foreach (DataRow dr in dt.Rows)
            {
                BlogDataModel item = new BlogDataModel
                {
                    blog_id = Convert.ToInt32(dr["blog_id"]),
                    blog_title = Convert.ToString(dr["blog_title"]),
                    blog_authour = Convert.ToString(dr["blog_authour"]),
                    blog_content = Convert.ToString(dr["blog_content"]),
                };
                lst.Add(item);
            }

            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {

            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = "select * from tbl_blog where blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();
            if (dt.Rows.Count == 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            DataRow row = dt.Rows[0];

            BlogDataModel item = new BlogDataModel
            {
                blog_id = Convert.ToInt32(row["blog_id"]),
                blog_title = Convert.ToString(row["blog_title"]),
                blog_authour = Convert.ToString(row["blog_authour"]),
                blog_content = Convert.ToString(row["blog_content"]),
            };
            lst.Add(item);


            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Ok(model);
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
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_title", blog.blog_title);
            cmd.Parameters.AddWithValue("@blog_authour", blog.blog_authour);
            cmd.Parameters.AddWithValue("@blog_content", blog.blog_content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = blog
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {

            string query = @"UPDATE [dbo].[Tbl_blog]
                        SET [blog_title] = @blog_title,
                            [blog_authour] = @blog_authour,
                            [blog_content] = @blog_content
                        WHERE [blog_id] = @blog_id";
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", blog.blog_id);
            cmd.Parameters.AddWithValue("@blog_title", blog.blog_title);
            cmd.Parameters.AddWithValue("@blog_authour", blog.blog_authour);
            cmd.Parameters.AddWithValue("@blog_content", blog.blog_content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = blog
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from tbl_blog where blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            List<BlogDataModel> lst = new List<BlogDataModel>();
            if (dt.Rows.Count == 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            DataRow row = dt.Rows[0];

            BlogDataModel item = new BlogDataModel
            {
                blog_id = Convert.ToInt32(row["blog_id"]),
                blog_title = Convert.ToString(row["blog_title"]),
                blog_authour = Convert.ToString(row["blog_authour"]),
                blog_content = Convert.ToString(row["blog_content"]),
            };
            lst.Add(item);
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.blog_title))
            {
                conditions += " [blog_title] =@blog_title, ";
                item.blog_title = blog.blog_title;
            }
            if (!string.IsNullOrEmpty(blog.blog_authour))
            {
                conditions += " [blog_authour] =@blog_authour, ";
                item.blog_authour = blog.blog_authour;
            }
            if (!string.IsNullOrEmpty(blog.blog_content))
            {
                conditions += " [blog_content] =@blog_content, ";
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

            using SqlCommand cmd2 = new SqlCommand(query, connection);
   
            cmd.Parameters.AddWithValue("@blog_title", blog.blog_title);
            cmd.Parameters.AddWithValue("@blog_authour", blog.blog_authour);
            cmd.Parameters.AddWithValue("@blog_content", blog.blog_content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful" : " Updating failed.",
                Data = lst
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_blog]
                           WHERE blog_Id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful" : " Deleting failed.",
            };

            return Ok(model);
        }

    }


}

