using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TransactionalAPIMaddiApp.Repository.Procedure;
using Microsoft.OpenApi.Models;
using TransactionalAPIMaddiApp.Helpers.Token;
using TransactionalAPIMaddiApp.Helpers.Mail;
using TransactionalAPIMaddiApp.Repository.Account;
using TransactionalAPIMaddiApp.Repository.Headquarters;
using TransactionalAPIMaddiApp.Repository.Restaurant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token de autorización en el siguiente formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddTransient<IRepositoryProcedure, RepositoryProcedure>();
builder.Services.AddTransient<IRepositoryAccount, RepositoryAccount>();
builder.Services.AddTransient<IRepositoryHeadquarters, RepositoryHeadquarters>();
builder.Services.AddTransient<IRepositoryRestaurant, RepositoryRestaurant>();
builder.Services.AddTransient<IMailHelper, MailHelper>();
builder.Services.AddTransient<ITokenHelper, TokenHelper>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        });
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("PolicyCore", builder =>
//    {
//        builder.WithOrigins("https://piloncito.maddiapp.com", "https://maddiapp.com")
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyCore", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PolicyCore");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
