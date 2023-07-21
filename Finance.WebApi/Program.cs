using Finance.Domain.Interfaces.Generics;
using Finance.Domain.Interfaces.ICategory;
using Finance.Domain.Interfaces.IExpense;
using Finance.Domain.Interfaces.IFinancialSystem;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Domain.Interfaces.IUserFinancialSystem;
using Finance.Domain.Services;
using Finance.Entities.Entities;
using Finance.Infra.Configuration;
using Finance.Infra.Repository;
using Finance.Infra.Repository.Generics;
using Finance.WebApi.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { 
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Financial System - API",
        Version = "v1",
        Description = "Financial System",
        Contact = new OpenApiContact
        {
            Name = "Equipe",
            Email = "info@konoha",
            Url = new Uri("http://www.google.com.br")
        }
    });
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Insert **ONLY** your JWT Bearer Token in the box bellow.",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        jwtSecurityScheme, Array.Empty<string>()
      }
    });
});

builder.Services.AddDbContext<ContextBase>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();

//Interface and Repository
builder.Services.AddSingleton(typeof(IGeneric<>), typeof(GenericsRepository<>));
builder.Services.AddSingleton<ICategory, CategoryRepository>();
builder.Services.AddSingleton<IExpense, ExpenseRepository>();
builder.Services.AddSingleton<IFinancialSystem, FinancialSystemRepository>();
builder.Services.AddSingleton<IUserFinancialSystem, UserFinancialSystemRepository>();

//DOMAIN.SERVICE
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();
builder.Services.AddSingleton<IFinancialSystemService, FinancialSystemService>();
builder.Services.AddSingleton<IUserFinancialSystemService, UserFinancialSystemService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(option =>
             {
                 option.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = "Teste.Securiry.Bearer",
                     ValidAudience = "Teste.Securiry.Bearer",
                     IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                 };

                 option.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                         return Task.CompletedTask;
                     }
                 };
             });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
