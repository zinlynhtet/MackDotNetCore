namespace MackDotNetCore.ConsoleApp.Model
{
	public class BlogResponseModel
	{
		public bool IsSuccess { get; set; }

		public string Message { get; set; }

		public BlogDataModel Data { get; set; }
	}
}
