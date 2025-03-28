using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlite("Data Source=app.db;Cache=Shared"));

// Configure Swagger services
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "CRUD Studies API",
    Version = "v1",
    Description = "API documentation for the CRUD Studies project",
    Contact = new OpenApiContact
    {
      Name = "Winnícius ",
      Email = "winnicius.moura@gmail.com",
      Url = new Uri("https://winnicius.pro")
    }
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD Studies API v1");
    c.RoutePrefix = string.Empty; 
  });
}

app.UseRouting();
app.MapControllers();

app.Run();