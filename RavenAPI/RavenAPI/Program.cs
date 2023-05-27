using Microsoft.EntityFrameworkCore;
using RavenDAL.Data;
using RavenDAL.Models;
using RavenDAL.Interface;
using RavenDAL.Repository;
using RavenBAL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations;
using Nest;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddTransient<RavenDAL.Interface.IRepository<Material>, RepositoryMaterial>();
builder.Services.AddTransient<MaterialServices, MaterialServices>();

builder.Services.AddDbContext<RavenDBContext>(options
=> options.UseSqlServer("Data Source=localhost; Initial Catalog=RavenDB; Persist Security Info=True; User Id=SA; Password=FR*@ger12"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Raven", Version = "v1" });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


