﻿using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.FluentValidations.Categories;
using LibrarySystem.Domain.IRepository;
using LibrarySystem.Domain.Mappings;
using LibrarySystem.Services.Services.ApplicationUsers;
using LibrarySystem.Services.Services.Authors;
using LibrarySystem.Services.Services.Books;
using LibrarySystem.Services.Services.Categories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Library.Web
{
    public static class DI
    {
        public static IServiceCollection ServicesInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorServices, AuthorServices>();
            services.AddScoped<IAuthServices, AuthServices>();



            return services
                .AuthenticationInjection(configuration)
                .ValidatorsInjection()
                .MappingsInjection();
        }

        private static IServiceCollection AuthenticationInjection(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(nameof(JwtOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    var jwtSettings=configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings!.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                        ValidateLifetime = true
                    };
                });
            
            return services;
        }
        private static IServiceCollection ValidatorsInjection(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CategoryRequestValidator).Assembly);
            return services;
        }
        private static IServiceCollection MappingsInjection(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookMapping).Assembly);
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            return services;
        }




    }
}