using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class ManagerPermissions
{
    public static string Type = "permissions";

    // Book Permissions
    public static string GetBooks { get; set; } = "books:get";
    public static string CreateBook { get; set; } = "books:create";
    public static string UpdateBook { get; set; } = "books:update";
    public static string DeleteBook { get; set; } = "books:delete";

    // fines permissions
    public static string GetFines { get; set; } = "fines:get";
    public static string CreateFine { get; set; } = "fines:create";
    public static string UpdateFine { get; set; } = "fines:update";
    public static string DeleteFine { get; set; } = "fines:delete";

    // Borrowed Books Permissions
    public static string GetBorrowedBooks { get; set; } = "borrowedBooks:get";
    public static string CreateBorrowedBooks { get; set; } = "borrowedBooks:create";
    public static string ReturnBorrowedBooks { get; set; } = "borrowedBooks:return";
    public static string DeleteBorrowedBooks { get; set; } = "borrowedBooks:delete";
    // Category Permissions
    public static string GetCategories { get; set; } = "categories:get";
    public static string CreateCategory { get; set; } = "categories:create";
    public static string UpdateCategory { get; set; } = "categories:update";
    public static string DeleteCategory { get; set; } = "categories:delete";

    // Author Permissions
    public static string GetAuthors { get; set; } = "authors:get";
    public static string CreateAuthor { get; set; } = "authors:create";
    public static string UpdateAuthor { get; set; } = "authors:update";
    public static string DeleteAuthor { get; set; } = "authors:delete";

    // Cart Permissions
    public static string GetCarts { get; set; } = "carts:get";
    public static string AddToCart { get; set; } = "carts:add";
    public static string RemoveFromCart { get; set; } = "carts:remove";

    // Order Permissions
    public static string GetOrders { get; set; } = "orders:get";
    public static string CreateOrder { get; set; } = "orders:create";
    public static string UpdateOrder { get; set; } = "orders:update";
    public static string DeleteOrder { get; set; } = "orders:delete";

    // Payment Permissions
    public static string GetPayments { get; set; } = "payments:get";
    public static string CreatePayment { get; set; } = "payments:create";
    public static string UpdatePayment { get; set; } = "payments:update";
    public static string DeletePayment { get; set; } = "payments:delete";
    // User Permissions
    public static string GetUser { get; set; } = "users:get";
    public static string CreateUser { get; set; } = "users:create";
    public static string UpdateUser { get; set; } = "users:update";
    public static string DeleteUser { get; set; } = "users:delete";
    // Role Permissions
    public static string GetRoles { get; set; } = "roles:get";
    public static string CreateRole { get; set; } = "roles:create";
    public static string UpdateRole { get; set; } = "roles:update";
    public static string DeleteRole { get; set; } = "roles:delete";

    public static IEnumerable<string?> AllPermissions =>
        typeof(ManagerPermissions).GetProperties()
            .Select(p => p.GetValue(null)! as string)
            .Where(p => p != null);


}

