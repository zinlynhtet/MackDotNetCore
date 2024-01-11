using Microsoft.AspNetCore.Mvc;
using MackDotNetCore.MinimalApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

// Apply migrations and ensure the database is created
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var dbContext = services.GetRequiredService<AppDbContext>();
	dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
{
	var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.blog_id == id);
	return Results.Ok(blog);
})
.WithName("GetBlogs")
.WithOpenApi();

app.Run();
