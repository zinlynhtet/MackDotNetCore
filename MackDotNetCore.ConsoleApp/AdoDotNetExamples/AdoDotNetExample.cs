using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.AdoDotNetExamples
{

    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder sqlconnectionStringBuilder;

        public AdoDotNetExample()
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
            Read();
            Create("test199", "authour1", "content1");
            Edit(5);
            Edit(20);
            Update(5, "Hello Mom", "KOko", "Greet");
            Delete(7);
        }
        private void Read()
        {
            #region Read / Retrieve


            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            Console.WriteLine("Connection opening...");
            connection.Open();
            Console.WriteLine("Connection opened...");

            string query = "select * from tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            Console.WriteLine("Connection closing...");
            connection.Close();
            Console.WriteLine("Connection closed...");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["blog_id"]);
                Console.WriteLine(row["blog_title"]);
                Console.WriteLine(row["blog_authour"]);
                Console.WriteLine(row["blog_content"]);

            }
        }
        #endregion
        private void Edit(int id)
        {

            #region Edit

            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = "select * from tbl_blog where blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }
            DataRow row = dt.Rows[0];

            Console.WriteLine(row["blog_id"]);
            Console.WriteLine(row["blog_title"]);
            Console.WriteLine(row["blog_authour"]);
            Console.WriteLine(row["blog_content"]);


        }
        #endregion
        private void Create(string title, string authour, string content)
        {
            #region Create

            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_blog]
           ([blog_title]
           ,[blog_authour]
           ,[blog_content])
     VALUES
           (@blog_title,
		   @blog_authour,
		   @blog_content)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_title", title);
            cmd.Parameters.AddWithValue("@blog_authour", authour);
            cmd.Parameters.AddWithValue("@blog_content", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful" : "Saving failed.";
            Console.WriteLine(message);
        }
        #endregion
        private void Update(int id, string title, string authour, string content)
        {
            #region Update
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_blog]
   SET [blog_title] = @blog_title
      ,[blog_authour] = @blog_authour
      ,[blog_content] = @blog_content
 WHERE blog_Id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", id);
            cmd.Parameters.AddWithValue("@blog_title", title);
            cmd.Parameters.AddWithValue("@blog_authour", authour);
            cmd.Parameters.AddWithValue("@blog_content", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "updating successful" : "updating failed";
            Console.WriteLine(message);
        }
        #endregion
        private void Delete(int id)
        {
            #region Delete
            SqlConnection connection = new SqlConnection(sqlconnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_blog]
                           WHERE blog_Id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blog_id", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            Console.WriteLine(message);
        }
        #endregion
    }
}
