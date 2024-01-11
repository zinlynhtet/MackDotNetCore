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
		}

		private async Task Read()
		{
			var model = await blogApi.GetBlogLists();
			foreach (var item in model.Data)
			{
				Console.WriteLine(item.blog_id);
				Console.WriteLine(item.blog_title);
				Console.WriteLine(item.blog_authour);
				Console.WriteLine(item.blog_content);
			}
		}
		//private async Task Edit(int id)
		//{
		//	var model = await blogApi.GetBlogList(id);
		//	var item = model;
		//		Console.WriteLine(item.blog_id);
		//		Console.WriteLine(item.blog_title);
		//		Console.WriteLine(item.blog_authour);
		//		Console.WriteLine(item.blog_content);
		//}
		//private async Task Create(string title, string authour, string content)
		//{
		//	BlogDataModel blog = new BlogDataModel
		//	{
		//		blog_title = title,
		//		blog_authour = authour,
		//		blog_content = content
		//	};
		//	var model = await blogApi.CreateBlogList(blog);
  //          await Console.Out.WriteLineAsync(model.Message);
  //      }
		//public async Task Update(int id, string title, string authour, string content)
		//{
		//	BlogDataModel blog = new BlogDataModel
		//	{
		//		blog_title = title,
		//		blog_authour = authour,
		//		blog_content = content
		//	};
		//	var model = await blogApi.UpdateBlogList(blog);
		//	await Console.Out.WriteLineAsync(model.Message);
		//}
		//public async Task Delete(int id)
		//{
		//	var model = await blogApi.DeleteBlogList(id);
		//	await Console.Out.WriteLineAsync(model.Message);
		//}
	}
	
}
