namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class SellerPermissions
{
    public static string Type = "permissions";

    public static string GetBooks { get; set; } = "books:get";
    public static string CreateBook { get; set; } = "books:create";
    public static string UpdateBook { get; set; } = "books:update";
    public static string DeleteBook { get; set; } = "books:delete";

    public static string GetBorrowedBooks { get; set; } = "borrowedBooks:get";
    public static string ReturnBorrowedBooks { get; set; } = "borrowedBooks:return";


    public static string GetCategories { get; set; } = "categories:get";

    public static string GetAuthors { get; set; } = "authors:get";

    public static string GetCarts { get; set; } = "carts:get";
    public static string AddToCart { get; set; } = "carts:add";
    public static string RemoveFromCart { get; set; } = "carts:remove";

    public static string GetOrders { get; set; } = "orders:get";
    public static string UpdateOrder { get; set; } = "orders:update"; // For order status updates

    public static string GetPayments { get; set; } = "payments:get";

    public static IEnumerable<string?> AllPermissions =>
        typeof(SellerPermissions).GetProperties()
            .Select(p => p.GetValue(null)! as string)
            .Where(p => p != null);
}
