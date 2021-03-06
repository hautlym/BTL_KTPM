using BTL_KTPM.Application.Catalog.Carts;
using BTL_KTPM.Application.Catalog.Categories;
using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Contacts;
using BTL_KTPM.Application.Catalog.Orders;
using BTL_KTPM.Application.Catalog.Producers;
using BTL_KTPM.Application.Catalog.Products;
using BTL_KTPM.Application.Catalog.System;
using BTL_KTPM.Application.Catalog.System.Dtos;
using BTL_KTPM.Application.Catalog.Users;
using BTL_KTPM.Application.Validate;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("192.168.1.14"), 5003));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
//    c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger BTL_KTPM", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      \r\n\r\nExample: 'Bearer 12345abcdef'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//                  {
//                    {
//                      new OpenApiSecurityScheme
//                      {
//                        Reference = new OpenApiReference
//                          {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                          },
//                          Scheme = "oauth2",
//                          Name = "Bearer",
//                          In = ParameterLocation.Header,
//                        },
//                        new List<string>()
//                      }
//    }
//    );
//}

                    );

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
builder.Services.AddTransient<IManageContact, ManageContact>();
builder.Services.AddTransient<IManageProducer, ManageProducer>();
builder.Services.AddTransient<IManageCart, ManagerCart>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<IManageOrder, ManageOrder>();
builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
string issuer = "https://hello.api.com";
string signingKey = "123456789987654321";
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidIssuer = issuer,
                   ValidateAudience = true,
                   ValidAudience = issuer,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ClockSkew = System.TimeSpan.Zero,
                   IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
               };
           });
        

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();