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
    public const string GetBooks    = "books:get";
    public const string CreateBook  = "books:create";
    public const string UpdateBook  = "books:update";
    public const string DeleteBook  = "books:delete";

    // fines permissions
    public const string GetFines    = "fines:get";
    public const string CreateFine  = "fines:create";
    public const string UpdateFine  = "fines:update";
    public const string DeleteFine  = "fines:delete";

    // Borrowed Books Permissions
    public const string GetBorrowedBooks     = "borrowedBooks:get";
    public const string CreateBorrowedBooks  = "borrowedBooks:create";
    public const string ReturnBorrowedBooks  = "borrowedBooks:return";
    public const string DeleteBorrowedBooks  = "borrowedBooks:delete";
    // Category Permissions
    public const string GetCategories  = "categories:get";
    public const string CreateCategory = "categories:create";
    public const string UpdateCategory = "categories:update";
    public const string DeleteCategory = "categories:delete";

    // Author Permissions
    public const string GetAuthors    = "authors:get";
    public const string CreateAuthor  = "authors:create";
    public const string UpdateAuthor  = "authors:update";
    public const string DeleteAuthor  = "authors:delete";

    // Cart Permissions
    public const string GetCarts = "carts:get";
    public const string ClearCarts = "carts:clear";
    public const string OperationOnCart = "carts:operation"; // add, remove, plus, minus

    // Order Permissions
    public const string GetOrders = "orders:get";
    public const string CreateOrder  = "orders:create";
    public const string UpdateOrder  = "orders:update";
    public const string DeleteOrder  = "orders:delete";

    // Payment Permissions
    public const string GetPayments = "payments:get";
    public const string CreatePayment = "payments:create";
    public const string UpdatePayment = "payments:update";
    public const string DeletePayment = "payments:delete";
    // User Permissions
    public const string GetUser = "users:get";
    public const string CreateUser = "users:create";
    public const string UpdateUser = "users:update";
    public const string DeleteUser = "users:delete";
    // Role Permissions
    public const string GetRoles = "roles:get";
    public const string CreateRole = "roles:create";
    public const string UpdateRole = "roles:update";
    public const string DeleteRole = "roles:delete";

    public static IEnumerable<string?> AllPermissions =>
        typeof(ManagerPermissions).GetFields()
            .Select(p => p.GetValue(null)! as string)
            .Where(p => p != null);


}

