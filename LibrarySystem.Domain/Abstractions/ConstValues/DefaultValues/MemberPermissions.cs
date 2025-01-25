namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class MemberPermissions
{
    public static string Type { get; }= "permissions";

    // Book Permissions
    public  const string GetBooks  = "books:get";
    public  const string ReturnBorrowedBooks  = "borrowedBooks:return";
    public  const string GetCategories  = "categories:get";
                   
    public  const string GetAuthors  = "authors:get";
             
    public  const string GetCarts  = "carts:get";
    public  const string ClearCarts = "carts:clear";
    public  const string OperationOnCart = "carts:operation"; // add, remove, plus, minus

    public const string GetOrders = "orders:get";
    public const string CreateOrder = "orders:create";
    public const string UpdateOrder = "orders:update";
    public const string DeleteOrder = "orders:delete";

    public const string CreatePayment = "payments:create";


    public const string GetReviews = "reviews:get";
    public const string AddReview = "reviews:add";
    public const string UpdateReview = "reviews:update";
    public const string DeleteReview = "reviews:delete";


    // this way is flixable but less performance than the commented way
    public static IEnumerable<string?> AllPermissions =>
        typeof(MemberPermissions).GetFields()
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
