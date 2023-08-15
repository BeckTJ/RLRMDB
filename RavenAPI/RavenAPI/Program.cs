using Microsoft.EntityFrameworkCore;
using RavenDAL.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenDAL.DTORepo;
using RavenBAL.Interface;
using RavenBAL.src;
using RavenBAL.Repository;
using RavenDAL.Models;
using RavenDAL.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

#region Service Injected
builder.Services.AddTransient <IRepository<RawMaterial>, RepositoryRawMaterial> ();
builder.Services.AddTransient<IRawMaterial<RawMaterialDTO>, RepoRawMaterial>();
builder.Services.AddTransient<IMaterialData<MaterialDataDTO>, RepoMaterialDataDTO>();
builder.Services.AddTransient<IVendor<VendorBatchDTO>, RepoVendorBatchDTO>();
builder.Services.AddTransient<ISampleSubmit<SampleDTO>, RepoSampleDTO>();

builder.Services.AddTransient<IProductId,ProductId>();
builder.Services.AddTransient<IVendorLot<VendorLot>, RepoVendorLot>();
builder.Services.AddTransient<IRawMaterialDrum<RawMaterialDrum>, RepoRawMaterialDrum>();
builder.Services.AddTransient<ISample<Sample>, RepoSample>();
builder.Services.AddTransient<IMaterial<MaterialInfo>, RepoMaterial>();
#endregion


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


