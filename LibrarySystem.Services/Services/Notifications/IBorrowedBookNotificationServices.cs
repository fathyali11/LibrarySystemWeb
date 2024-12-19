namespace LibrarySystem.Services.Services.Notifications;
public interface IBorrowedBookNotificationServices
{
    Task SendNotificationToBorrower();
}
