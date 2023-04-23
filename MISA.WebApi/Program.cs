using MISA.BLL.Interface;
using MISA.BLL.Services;
using MISA.DL.Interface;
using MISA.DL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// DL interface
/// </summary>
builder.Services.AddScoped<IRepository<object>, BaseRepository<object>>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetCategoryRepository, AssetCategoryRepository>();
builder.Services.AddScoped<ICostCourceRepository, CostCourceRepository>();
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
builder.Services.AddScoped<ILicenseDetailRepository, LicenseDetailRepository>();

/// <summary>
/// BLL Interface
/// </summary>
builder.Services.AddScoped<IBaseBLL<object>, BaseBLL<object>>();
builder.Services.AddScoped<IDepartmentBLL, DepartmentBLL>();
builder.Services.AddScoped<IAssetBLL, AssetBLL>();
builder.Services.AddScoped<IAssetCategoryBLL, AssetCategoryBLL>();
builder.Services.AddScoped<ICostSourceBLL, CostCourceBLL>();
builder.Services.AddScoped<ILicenseBLL, LicenseBLL>();
builder.Services.AddScoped<ILicenseDetailBLL, LicenseDetailBLL>();

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseAuthorization();

app.MapControllers();

app.Run();
