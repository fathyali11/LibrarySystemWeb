using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using LibrarySystem.Domain.Mappings;
using LibrarySystem.Services.Services.Books;
using LibrarySystem.Services.Services.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using LibrarySystem.Domain.FluentValidations.Categories;
using LibrarySystem.Services.Services.Authors;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(BookMapping).Assembly);
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(typeof(CategoryRequestValidator).Assembly);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorServices, AuthorServices>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
