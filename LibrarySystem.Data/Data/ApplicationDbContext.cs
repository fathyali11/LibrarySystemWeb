﻿using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibrarySystem.Data.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.ConfigureWarnings(warnings =>
    //    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    //}
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<BorrowedBook> BorrowedBooks { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;
    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;
    public DbSet<Cart> Carts { get; set; } = default!;
    public DbSet<CartItem> CartItems { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Fine> Fines { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;


}
