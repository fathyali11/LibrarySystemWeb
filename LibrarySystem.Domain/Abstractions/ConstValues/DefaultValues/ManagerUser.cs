namespace LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
public static class ManagerUser
{
    public static string Name = "Manager";

    public static string Id = "db87724c-6e28-4a90-a1fd-3d8fe88475e6";
    public static string UserName = "Manager";
    public static string FirstName = "Fathy";
    public static string LastName = "Ali";
    public static string Address = "Mansoura";
    public static string NormalizedUserName =UserName.ToUpper();
    public static string Email = "fathy.ali8ali@gmail.com";
    public static string NormalizedEmail =Email.ToUpper();
    public static bool EmailConfirmed =true;
    public static string Password = "Ff01009927@";
    public static string SecurityStamp = "c5461f60-2df9-4235-a040-0ffcfb579c41";
    public static string ConcurrencyStamp = "6a4590fd-cddc-4eed-b1c8-e01ddce5c064";
    public static string PhoneNumber = "01009927286";
    public static bool PhoneNumberConfirmed = false;
    public static bool TwoFactorEnabled =false;
    public static DateTimeOffset? LockoutEnd { get; set; } 
    public static bool LockoutEnabled { get; set; }
    public static int AccessFailedCount { get; set; }

}
