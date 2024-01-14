using MackDotNetCore.ConsoleApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.RefitExamples
{
	public class RefitExample
	{
		private readonly IBlogApi blogApi = RestService.For<IBlogApi>("http://localhost:5095");
		public async Task Run()
		{
			await Read();
			await Edit(22);
			//await Create("Hello", "this", "isRefit");
			//await Update(17, "this", "is", "updateRefit");
			//await Delete(50);
		}

		private async Task Read()
		{
			var model = await blogApi.GetBlogLists();
			if (model != null && model.Data != null)
			{
				foreach (var item in model.Data)
				{
					Console.WriteLine(item.blog_id);
					Console.WriteLine(item.blog_title);
					Console.WriteLine(item.blog_authour);
					Console.WriteLine(item.blog_content);
				}
			}
			else
			{
				Console.WriteLine("No data returned from the server.");
			}
		}
		private async Task Edit(int id)
		{
			var model = await blogApi.Getbloglist(id);
			if (model != null)
			{
				var item = model;
				Console.WriteLine(item.blog_id);
				Console.WriteLine(item.blog_title);
				Console.WriteLine(item.blog_authour);
				Console.WriteLine(item.blog_content);
			}
			else
			{
				Console.WriteLine("No data returned from the server.");
			}
		}
		private async Task Create(string title, string authour, string content)
		{
			BlogDataModel blog = new BlogDataModel
			{
				blog_title = title,
				blog_authour = authour,
				blog_content = content
			};
			var model = await blogApi.CreateBlogList(blog);
			await Console.Out.WriteLineAsync(model.Message);
		}
		public async Task Update(int id, string title, string authour, string content)
		{
			BlogDataModel blog = new BlogDataModel
			{
				blog_id = id,
				blog_title = title,
				blog_authour = authour,
				blog_content = content
			};

			var model = await blogApi.UpdateBlogList(id, blog);
			await Console.Out.WriteLineAsync(model.Message);
		}

		private async Task Delete(int id)
		{
			var model = await blogApi.Getbloglist(id);
			var result = await blogApi.DeleteBlog(id);
			if (result != null)
			{
				await Console.Out.WriteLineAsync("Deleting successful.");
			}
			else
			{
				await Console.Out.WriteLineAsync("Deleting Failed.");
			}
		}
	}
}
