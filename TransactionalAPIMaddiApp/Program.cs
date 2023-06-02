using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TransactionalAPIMaddiApp.Helpers.Token;
using TransactionalAPIMaddiApp.Helpers.Mail;
using TransactionalAPIMaddiApp.Repository.Account;
using TransactionalAPIMaddiApp.Repository.Headquarters;
using TransactionalAPIMaddiApp.Helpers.File;
using TransactionalAPIMaddiApp.Repository.Restaurant;
using TransactionalAPIMaddiApp.Helpers.Sql;
using TransactionalAPIMaddiApp.Repository.Category;
using Microsoft.Extensions.FileProviders;
using TransactionalAPIMaddiApp.Repository.SubCategory;
using TransactionalAPIMaddiApp.Repository.Product;

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
builder.Services.AddTransient<IRepositoryAccount, RepositoryAccount>();
builder.Services.AddTransient<IRepositoryHeadquarters, RepositoryHeadquarters>();
builder.Services.AddTransient<IRepositoryRestaurant, RepositoryRestaurant>();
builder.Services.AddTransient<IRepositoryCategory, RepositoryCategory>();
builder.Services.AddTransient<IRepositorySubCategory, RepositorySubCategory>();
builder.Services.AddTransient<IRepositoryProduct, RepositoryProduct>();
builder.Services.AddTransient<ISqlHelper, SqlHelper>();
builder.Services.AddTransient<IMailHelper, MailHelper>();
builder.Services.AddTransient<ITokenHelper, TokenHelper>();
builder.Services.AddTransient<IFileHelper, FileHelper>();
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "AssetsImage")),
    RequestPath = "/AssetsImage"
});

app.UseHttpsRedirection();

app.UseCors("PolicyCore");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
