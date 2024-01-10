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
	}
}
