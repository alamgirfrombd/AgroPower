using AgroPower.Persistence.Repositories;
using AgroPower.Persistence;
using Microsoft.EntityFrameworkCore;
using AgroPower.Domain.Interfaces;
using AgroPower.Application.Services;
using AgroPower.Application.Interfaces;
using AgroPower.Application.Mappings;
using AgroPower.UILayer.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AgroPowerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 🛠 Razor View & Controller config (before build)
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(HomeController).Assembly)
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/UILayer/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/UILayer/Views/Shared/{0}.cshtml");
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔨 Build after all service registration
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
