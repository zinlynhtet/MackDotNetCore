using MackDotNetCore.MinimalApi;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
	options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	options.SerializerOptions.PropertyNamingPolicy = new UpperCaseNamingPolicy(); 
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
	string? connectionString = builder.Configuration.GetConnectionString("DbConnection");
	options.UseSqlServer(connectionString);
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddBlogService();
app.Run();

public class UpperCaseNamingPolicy : JsonNamingPolicy
{
	public override string ConvertName(string name)
	{
		if (string.IsNullOrEmpty(name))
			return name;

		char[] chars = name.ToCharArray();
		chars[0] = char.ToUpper(chars[0]);
		return new string(chars);
	}
}