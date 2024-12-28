namespace LibrarySystem.Services.Services.Notifications;
public interface IBorrowedBookNotificationServices
{
    Task SendFineNotificationToBorrower();
    Task SendReminderNotificationToBorrower();
}
