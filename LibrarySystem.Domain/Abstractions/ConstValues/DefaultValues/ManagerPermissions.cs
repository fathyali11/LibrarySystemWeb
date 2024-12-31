using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class ManagerPermissions
{
    public static string Type { get; } ="permissions";

    // Book Permissions
    public static string GetBooks    = "books:get";
    public static string CreateBook  = "books:create";
    public static string UpdateBook  = "books:update";
    public static string DeleteBook  = "books:delete";

    // fines permissions
    public static string GetFines    = "fines:get";
    public static string CreateFine  = "fines:create";
    public static string UpdateFine  = "fines:update";
    public static string DeleteFine  = "fines:delete";

    // Borrowed Books Permissions
    public static string GetBorrowedBooks     = "borrowedBooks:get";
    public static string CreateBorrowedBooks  = "borrowedBooks:create";
    public static string ReturnBorrowedBooks  = "borrowedBooks:return";
    public static string DeleteBorrowedBooks  = "borrowedBooks:delete";
    // Category Permissions
    public static string GetCategories  = "categories:get";
    public static string CreateCategory = "categories:create";
    public static string UpdateCategory = "categories:update";
    public static string DeleteCategory = "categories:delete";

    // Author Permissions
    public static string GetAuthors    = "authors:get";
    public static string CreateAuthor  = "authors:create";
    public static string UpdateAuthor  = "authors:update";
    public static string DeleteAuthor  = "authors:delete";

    // Cart Permissions
    public static string GetCarts      = "carts:get";
    public static string AddToCart    = "carts:add";
    public static string RemoveFromCart  = "carts:remove";

    // Order Permissions
    public static string GetOrders = "orders:get";
    public static string CreateOrder  = "orders:create";
    public static string UpdateOrder  = "orders:update";
    public static string DeleteOrder  = "orders:delete";

    // Payment Permissions
    public static string GetPayments = "payments:get";
    public static string CreatePayment = "payments:create";
    public static string UpdatePayment = "payments:update";
    public static string DeletePayment = "payments:delete";
    // User Permissions
    public static string GetUser = "users:get";
    public static string CreateUser = "users:create";
    public static string UpdateUser = "users:update";
    public static string DeleteUser = "users:delete";
    // Role Permissions
    public static string GetRoles = "roles:get";
    public static string CreateRole = "roles:create";
    public static string UpdateRole = "roles:update";
    public static string DeleteRole = "roles:delete";

    public static IEnumerable<string?> AllPermissions =>
        typeof(ManagerPermissions).GetFields()
            .Select(p => p.GetValue(null)! as string)
            .Where(p => p != null);


}

