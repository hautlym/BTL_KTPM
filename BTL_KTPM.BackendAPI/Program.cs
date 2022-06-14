using BTL_KTPM.Application.Catalog.Carts;
using BTL_KTPM.Application.Catalog.Categories;
using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products;
using BTL_KTPM.Application.Catalog.System;
using BTL_KTPM.Application.Catalog.Users;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("192.168.1.14"), 5003));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<BTL_KTPMDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("BTL_KTPM_Database")));
builder.Services.AddIdentity<AppUser, AppRoles>().AddEntityFrameworkStores<BTL_KTPMDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IPublicProductService, PublicProductService>();
builder.Services.AddTransient<IManageProductService, ManageProductService>();
builder.Services.AddTransient<IManageCategory, ManageCategory>();
//builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
//builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
//builder.Services.AddTransient<RoleManager<AppUser>, RoleManager<AppUser>>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IManagerUser, ManagerUser>();
builder.Services.AddTransient<IManageCart, ManagerCart>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();