namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class SellerPermissions
{
    public static string Type { get; }= "permissions";
    // convert all properties to cosnt fields      

    public const string GetBooks  = "books:get";
    public const string CreateBook  = "books:create";
    public const string UpdateBook  = "books:update";
    public const string DeleteBook = "books:delete";
    public const string GetBorrowedBooks  = "borrowedBooks:get";
    public const string ReturnBorrowedBooks  = "borrowedBooks:return";
           
    public const string GetCategories = "categories:get";
           
    public const string GetAuthors  = "authors:get";
           
    public const string GetCarts  = "carts:get";
    public const string AddToCart  = "carts:add";
    public const string RemoveFromCart  = "carts:remove";
           
    public const string GetOrders = "orders:get";
    public const string UpdateOrder  = "orders:update"; // For order status updates
           
    public const string GetPayments  = "payments:get";

    public static IEnumerable<string?> AllPermissions =>
        typeof(SellerPermissions).GetFields()
            .Select(p => p.GetValue(null)! as string)
            .Where(p => p != null);
}
