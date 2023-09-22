using Microsoft.EntityFrameworkCore;
using NLog;
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
using RavenAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
LogManager.Setup().LoadConfigurationFromFile("/nlog.config");

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureMSSqlContext(builder.Configuration);

builder.Services.AddControllers();
//builder.Services.AddHttpClient();


#region Service Injected
builder.Services.AddTransient <IRepository<RawMaterial>, RepositoryRawMaterial> ()
    .AddTransient<IRawMaterial<RawMaterialDTO>, RepoRawMaterial>()
    .AddTransient<IMaterialData<MaterialDataDTO>, RepoMaterialDataDTO>()
    .AddTransient<IVendor<VendorBatchDTO>, RepoVendorBatchDTO>()
    .AddTransient<ISampleSubmit<SampleDTO>, RepoSampleDTO>();

builder.Services.AddTransient<IProductId,ProductId>()
    .AddTransient<IVendorLot<VendorLot>, RepoVendorLot>()
    .AddTransient<IRawMaterialDrum<RawMaterialDrum>, RepoRawMaterialDrum>()
    .AddTransient<ISample<Sample>, RepoSample>()
    .AddTransient<IMaterial<MaterialInfo>, RepoMaterial>();
#endregion


//builder.Services.AddDbContext<RavenDBContext>(options
//=> options.UseSqlServer("Data Source=localhost; Initial Catalog=RavenDB; Persist Security Info=True; User Id=SA; Password=FR*@ger12"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Raven", Version = "v1" });
//});

var app = builder.Build();


//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


