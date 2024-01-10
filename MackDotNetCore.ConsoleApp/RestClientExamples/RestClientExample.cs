using MackDotNetCore.ConsoleApp.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.RestClientExamples
{
	internal class RestClientExample
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
			RestRequest request = new RestRequest("http://localhost:5095/api/blog/", Method.Get);
			RestClient client = new RestClient();
			var response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr =  response.Content!;
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
			RestRequest request = new RestRequest($"http://localhost:5095/api/blog/{id}", Method.Get);
			RestClient client = new RestClient();
			var response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr =  response.Content!;
				BlogDataModel? model = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
				var item = model!;
				Console.WriteLine(item.blog_id);
				Console.WriteLine(item.blog_title);
				Console.WriteLine(item.blog_authour);
				Console.WriteLine(item.blog_content);
			}
			else
			{
				string jsonStr = response.Content!;
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

			RestRequest request = new RestRequest("http://localhost:5095/api/blog/", Method.Post);
			request.AddJsonBody(blog);
			RestClient client = new RestClient();
			var response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = response.Content!;
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

			RestRequest request = new RestRequest($"http://localhost:5095/api/blog/{id}", Method.Put);
			request.AddJsonBody(blog);
			RestClient client = new RestClient();
			var response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = response.Content!;
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
			else
			{
				string jsonStr = response.Content!;
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
		}

		public async Task Delete(int id)
		{

			RestRequest request = new RestRequest($"http://localhost:5095/api/blog/{id}", Method.Delete);
			RestClient client = new RestClient();
			var response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = response.Content!;
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
			else
			{
				string jsonStr = response.Content!;
				var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
				Console.WriteLine(model!.Message);
			}
		}
	}
}
