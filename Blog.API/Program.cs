using Blog.Domain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Blog.Repository.Interfaces;
using Blog.Repository.Implementation;
using Blog.Services.Implementation;
using Blog.Services.Interfaces;
using Blog.Domain.Models;
using Blog.API.Mapping;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddDbContext<BlogDataContext>(options => options.UseSqlServer(ConnectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IGenericService<BlogPost>, BlogService>();
builder.Services.AddTransient<IBlogService, BlogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
