using MackDotNetCore.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDBContext _dbContext;
        public EFCoreExample() 
        { 
            _dbContext = new AppDBContext();
        }
        public void Run()
        {
            Read();
            //    Edit(2);
            //    Edit(1000);
            //    Create("test 8.50", "test 2", "test 3");
            //    Update(2004, "test 8.53", "test 2", "test 3");
            //    Delete(2004);
        }
        private void Read()
        {
            List<BlogDataModel> lst =_dbContext.Blogs.ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.blog_id);
                Console.WriteLine(item.blog_title);
                Console.WriteLine(item.blog_authour);
                Console.WriteLine(item.blog_content);
            }
        }
        private void Edit(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.blog_id);
            Console.WriteLine(item.blog_title);
            Console.WriteLine(item.blog_authour);
            Console.WriteLine(item.blog_content);
        }
        private void Create( string title ,string authour, string content)
        {
            BlogDataModel blog = new BlogDataModel 
            { 
                blog_title = title ,
                blog_authour = authour ,
                blog_content = content
            };
            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Savaing Failed.";
            Console.WriteLine(message);
            Console.WriteLine(blog.blog_id);
        }

        private void Update(int id, string title, string author, string content)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (blog is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            blog.blog_title = title;
            blog.blog_authour = author;
            blog.blog_content = content;

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.blog_id == id);
            if (blog is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            _dbContext.Blogs.Remove(blog);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
  
}
