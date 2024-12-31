namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class MemberPermissions
{
    public static string Type = "permissions";

    // Book Permissions
    public static string GetBooks { get; set; } = "books:get";
    public static string ReturnBorrowedBooks { get; set; } = "borrowedBooks:return";
    public static string GetCategories { get; set; } = "categories:get";

    public static string GetAuthors { get; set; } = "authors:get";

    public static string GetCarts { get; set; } = "carts:get";
    public static string ClearCarts { get; set; } = "carts:clear";
    public static string OperationOnCart { get; set; } = "carts:operation"; // add, remove, plus, minus

    public static string CreateOrder { get; set; } = "orders:create";
    public static string CancelOrder { get; set; } = "orders:cancel";

    public static string GetOrders { get; set; } = "orders:get";

    public static string CreatePayment { get; set; } = "payments:create";

    // this way is flixable but less performance than the commented way
    public static IEnumerable<string?> AllPermissions =>
        typeof(MemberPermissions).GetProperties()
            .Select(p => p.GetValue(null)! as string)
            .Where(p => p != null);

    //public static IEnumerable<string> AllPermissions => new List<string>
    //{
    //    GetBooks,
    //    GetCategories,
    //    ViewCart,
    //    AddItemToCart,
    //    RemoveItemFromCart,
    //    PlusItemInCart,
    //    MinusItemInCart,
    //    ClearCart,
    //    MakeOrder,
    //    CreateCheckout,
    //    ReturnBook
    //};


}
