using MackDotNetCore.ConsoleApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MackDotNetCore.ConsoleApp.HttpClientExamples
{
	public class HttpClientExample
	{
		public async Task Run()
		{
			//await Read();
			//await Edit(11);
			//await Edit(12);
			//await Create("test 8.50", "test 2", "HTTP CLIENT");
			//await Update(13, "Client", "Client", "Client");
			//await Delete(12);

		}

		public async Task Read()
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync("http://localhost:5095/api/blog");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
				foreach (var item in model!.Data)
				{
					Console.WriteLine(item.blog_id);
					Console.WriteLine(item.blog_title);
					Console.WriteLine(item.blog_authour);
					Console.WriteLine(item.blog_content);
				}
			}
		}

		public async Task Edit(int id)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync($"http://localhost:5095/api/blog/{id}");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				BlogDataModel? model = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
				var item = model!;
				Console.WriteLine(item.blog_id);
				Console.WriteLine(item.blog_title);
				Console.WriteLine(item.blog_authour);
				Console.WriteLine(item.blog_content);
			}
			else
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
		}

		public async Task Create(string title, string authour, string content)
		{

			BlogDataModel blog = new BlogDataModel
			{
				blog_title = title,
				blog_authour = authour,
				blog_content = content
			};

			string jsonBlog = JsonConvert.SerializeObject(blog);
			HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.PostAsync("http://localhost:5095/api/blog", httpContent);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				await Console.Out.WriteLineAsync(model!.Message);
			}
		}

		public async Task Update(int id, string title, string authour, string content)
		{
			BlogDataModel blog = new BlogDataModel
			{
				blog_title = title,
				blog_authour = authour,
				blog_content = content
			};

			string jsonBlog = JsonConvert.SerializeObject(blog);
			HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.PutAsync($"http://localhost:5095/api/blog/{id}", httpContent);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
			else
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
		}

		public async Task Delete(int id)
		{

			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5095/api/blog/{id}");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
			else
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
		}
	}
}

