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
using LibrarySystem.Services.Services.AuthUsers;
using LibrarySystem.Services.Services.Authors;
using LibrarySystem.Services.Services.Books;
using LibrarySystem.Services.Services.Categories;
using LibrarySystem.Services.Services.Emails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LibrarySystem.Services.Services.AccountUsers;
using LibrarySystem.Services.Services.Tokens;
using LibrarySystem.Services.Services.Cashing;
using LibrarySystem.Services.Services.CartItems;
using LibrarySystem.Services.Services.Carts;
using LibrarySystem.Services.Services.Orders;
using Hangfire;
using LibrarySystem.Services.Services.Notifications;
using LibrarySystem.Services.Services.Payments;
using LibrarySystem.Services.Services.Fines;
using LibrarySystem.Services.Services.BorrowedBooks;
using LibrarySystem.Services.CustomAuthorization;
using Microsoft.AspNetCore.Authorization;
using LibrarySystem.Services.Services.Users;
using LibrarySystem.Services.Services.Roles;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using System.Security.Claims;

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
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IAccountUserServices, AccountUserServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<ICartRepository,CartRepository>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<ICartItemServices, CartItemServices>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
            services.AddScoped<ICacheServices,CashServices>();
            services.AddScoped<IBorrowedBookNotificationServices, BorrowedBookNotificationServices>();
            services.AddScoped<IBorrowedBookServices, BorrowedBookServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IFineRepository, FineRepository>();
            services.AddScoped<IFineNotificationServices, FineNotificationServices>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, ApplicationAuthorizationPolicyProvider>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IUserServices, UserServices>();








#pragma warning disable
            services.AddHybridCache();
            #pragma warning restore


            return services
                .AuthenticationInjection(configuration)
                .ValidatorsInjection()
                .MappingsInjection()
                .EmailInjection(configuration)
                .ExceptionHandlerInjection()
                .HangfireInjection(configuration)
                .StripeInjection(configuration)
                .RateLimitingInjection();
        }

        private static IServiceCollection AuthenticationInjection(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
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
        private static IServiceCollection EmailInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EmailOptions>()
                .Bind(configuration.GetSection(nameof(EmailOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
        private static IServiceCollection ExceptionHandlerInjection(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }
        private static IServiceCollection HangfireInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config => config
                 .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                 .UseSimpleAssemblyNameTypeSerializer()
                 .UseRecommendedSerializerSettings()
                 .UseSqlServerStorage(configuration.GetConnectionString("JobsConnection")));

            services.AddHangfireServer();

            services.AddMvc();
            return services;    
        }
        private static IServiceCollection StripeInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<StripeSettings>()
                .Bind(configuration.GetSection(nameof(StripeSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            return services;
        }
        private static IServiceCollection RateLimitingInjection(this IServiceCollection services)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddPolicy
                ("IdLimiter", HttpContent =>
                     RateLimitPartition.GetFixedWindowLimiter
                         (
                             partitionKey: HttpContent.User.FindFirstValue(ClaimTypes.NameIdentifier),
                             factory: _ => new FixedWindowRateLimiterOptions
                             {
                                 Window = TimeSpan.FromSeconds(60),
                                 PermitLimit = 5
                             }
                         )
                );

                
                
                rateLimiterOptions.AddTokenBucketLimiter("token", tokenOptions =>
                {
                    tokenOptions.TokenLimit = 2;
                    tokenOptions.QueueLimit = 1;
                    tokenOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    tokenOptions.ReplenishmentPeriod = TimeSpan.FromSeconds(30);
                    tokenOptions.TokensPerPeriod = 2;
                    tokenOptions.AutoReplenishment = true;
                });
            });
            return services;
        }
    }
}
