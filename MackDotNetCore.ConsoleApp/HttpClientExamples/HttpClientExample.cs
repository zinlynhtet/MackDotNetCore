using MackDotNetCore.ConsoleApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.HttpClientExamples
{
	internal class HttpClientExample
	{
		public async Task Run()
		{
			await Read();
		}
		public async Task Read()
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response =  await client.GetAsync("http://localhost:5095/api/blog");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
				foreach ( var item in model.Data)
				{
					Console.WriteLine(item.blog_id);
					Console.WriteLine(item.blog_title);
					Console.WriteLine(item.blog_authour);
					Console.WriteLine(item.blog_content);
				}
			}
		}
		public async Task Edit()
		{

		}
		public async Task Create()
		{

		}
		public async Task Update()
		{

		}
		public async Task Delete()
		{

		}
	}
}
