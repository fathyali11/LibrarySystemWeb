namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public class SellerUser
{
    public static string Name = "Seller";

    public static string Id = "37597bc6-ffee-45d1-ad20-b53b66651c86";
    public static string UserName = "Seller";
    public static string FirstName = "Fathy";
    public static string LastName = "Ali";
    public static string Address = "Mansoura";
    public static string NormalizedUserName = UserName.ToUpper();
    public static string Email = "man.8man010099@gmail.com";
    public static string NormalizedEmail = Email.ToUpper();
    public static bool EmailConfirmed = true;
    public static string Password = "Ff01009927@";
    public static string SecurityStamp = "94288f43-d8d2-4f7a-ba4d-d6893b2ad1fe";
    public static string ConcurrencyStamp = "0955ece4-848c-4929-9bf1-dcd548d93f97";
    public static string PhoneNumber = "01556788707";
    public static bool PhoneNumberConfirmed = false;
    public static bool TwoFactorEnabled = false;
    public static DateTimeOffset? LockoutEnd { get; set; }
    public static bool LockoutEnabled { get; set; }
    public static int AccessFailedCount { get; set; }
}
