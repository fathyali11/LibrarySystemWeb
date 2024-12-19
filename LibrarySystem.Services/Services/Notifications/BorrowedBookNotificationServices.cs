using Newtonsoft.Json.Linq;

namespace LibrarySystem.Services.Services.Notifications;
public class BorrowedBookNotificationServices(IUnitOfWork unitOfWork,
    IEmailSender emailSender,
    IOptions<EmailOptions> options) : IBorrowedBookNotificationServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly EmailOptions _options = options.Value;

    public async Task SendNotificationToBorrower()
    {
        var borrowedBooks = await _unitOfWork.BorrowedBookRepository
            .GetAllWithUserAndBook(x => x.DueDate<DateTime.Now);

        foreach (var borrowedBook in borrowedBooks)
        {
            var user = borrowedBook.User;
            var bookTitle = borrowedBook.Book.Title;
            var dueDate = borrowedBook.DueDate;

            await SendNotificationEmail(user, bookTitle, dueDate);
        }
    }
    private async Task SendNotificationEmail(ApplicationUser user, string bookTitle, DateTime dueDate)
    {
        var keyValues = new Dictionary<string, string>()
        {
            {"{UserName}", user.FirstName + " " + user.LastName},
            {"{BookTitle}", bookTitle},
            {"{DueDate}", dueDate.ToString("MMMM dd, yyyy")},
            {"{Library Contact Information}", _options.SenderEmail }
        };
        var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "BorrowBookTemplate.html", keyValues);
        await _emailSender.SendEmailAsync(user.Email!, "Borrowed Book Notification", emailBody);
    }
}
