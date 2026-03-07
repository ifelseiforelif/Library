using Books.Application.Commands.CreateCountry;
using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Application.Mapping;
using Books.Application.Queries.GetAllCountries;
using Books.Application.Services;
using Books.Infrastructure.Configuration;
using Books.Infrastructure.Data;
using Books.Infrastructure.Helpers;
using Books.Infrastructure.Repositories;
using Books.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

namespace Books.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        // ================= JWT Settings =================
        var jwtSettings = configuration
            .GetSection("Jwt")
            .Get<JwtSettings>()
            ?? throw new Exception("JWT settings not configured.");

        builder.Services.Configure<JwtSettings>(
            configuration.GetSection("Jwt"));

        // ================= Database =================
        builder.Services.AddDbContext<LibraryDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        //builder.Services.AddDbContext<LibraryDbContext>(options =>
        //    options.UseMySql(
        //        configuration.GetConnectionString("ConnectionToMySql"),
        //        ServerVersion.AutoDetect(
        //            configuration.GetConnectionString("ConnectionToMySql")
        //        )));

        // ================= AutoMapper =================
        builder.Services.AddAutoMapper(
            _ => { },
            typeof(BookProfile).Assembly,
            typeof(AuthorProfile).Assembly,
            typeof(GenreProfile).Assembly,
            typeof(UserProfile).Assembly,
            typeof(CountryProfile).Assembly
        );
        // ================= CORS =================
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
        //==================MEDIATR======================
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateCountryHandler).Assembly);
           
        });
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetAllCountriesHandler).Assembly);

        });
        //===================CACHE=======================
        builder.Services.AddMemoryCache();
        // ================= Repositories =================
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IGenreRepository, GenreRepository>();
        builder.Services.AddScoped<ICountryRepository, CountryRepository>();

        //======================Redis=====================
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = builder.Configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(config);
        });

        // ================= Services =================
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IHashHelper, HashHelper>();
        // builder.Services.AddScoped<ICachingService, MemoryCachingService>();
        builder.Services.AddScoped<ICachingService, RedisCachingService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // ================= Swagger + JWT =================
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT token"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        // ================= Authentication =================
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Key)
                ),

                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();
        app.UseCors("AllowAll");

        // ================= Middleware =================
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
    }
}