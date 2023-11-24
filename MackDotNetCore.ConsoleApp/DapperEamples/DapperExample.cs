using Dapper;
using MackDotNetCore.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        public readonly SqlConnectionStringBuilder sqlconnectionStringBuilder;

        public DapperExample()
        {
            sqlconnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "MSI\\MSSQLSEVER", // sever name
                InitialCatalog = "ALTDotNetCore", //database name
                UserID = "sa",
                Password = "Queen@2001"

            };
        }
        public void Run()
        {
            //Read();
            //Create("test998", "authour2", "content12");
            //Edit(5);
            //Edit(20);
            //Update(10, "Hello Mom", "FLick", "Greeting");
            //Delete(4);
        }
        private void Read()
        {
            #region Read / Retrieve
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            List<dynamic> lst = db.Query(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.blog_id);
                Console.WriteLine(item.blog_title);
                Console.WriteLine(item.blog_authour);
                Console.WriteLine(item.blog_content);
            }

        }
        #endregion
        private void Edit(int id)
        {

            #region Edit
            BlogDataModel blog = new BlogDataModel()
            {
                blog_id = id
            };
            string query = "select * from tbl_blog where blog_id = @blog_id";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data found.");
                return;
            }

            Console.WriteLine(item.blog_id);
            Console.WriteLine(item.blog_title);
            Console.WriteLine(item.blog_authour);
            Console.WriteLine(item.blog_content);




        }
        #endregion
        private void Create(string title, string authour, string content)
        {
            #region Create
            BlogDataModel blog = new BlogDataModel()
            {
                blog_title = title,
                blog_authour = authour,
                blog_content = content
            };
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
            string message = result > 0 ? "Creating Successful" : "Creating failed.";
            Console.WriteLine(message);
        }
        #endregion
        private void Update(int id, string title, string authour, string content)
        {
            #region Update
            BlogDataModel blog = new BlogDataModel()
            {
                blog_id = id,
                blog_title = title,
                blog_authour = authour,
                blog_content = content
            };

            string query = @"UPDATE [dbo].[Tbl_blog]
   SET [blog_title] = @blog_title
      ,[blog_authour] = @blog_authour
      ,[blog_content] = @blog_content
 WHERE blog_Id = @blog_id";

            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating successful" : "Updating failed";
            Console.WriteLine(message);
        }
        #endregion
        private void Delete(int id)
        {
            #region Delete
            BlogDataModel blog = new BlogDataModel { blog_id = id };
            string query = @"DELETE FROM [dbo].[Tbl_blog]
                           WHERE blog_Id = @blog_id";
            using IDbConnection db = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            Console.WriteLine(message);
        }
        #endregion
    }
}

