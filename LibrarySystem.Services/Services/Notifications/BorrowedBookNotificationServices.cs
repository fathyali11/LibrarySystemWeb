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
            await SendNotificationEmail(borrowedBook.FirstName,
                borrowedBook.LastName,borrowedBook.Email, borrowedBook.BookTitle, borrowedBook.DueDate);
        }
    }
    private async Task SendNotificationEmail(string firstName,string lastName,string email, string bookTitle, DateTime dueDate)
    {
        var keyValues = new Dictionary<string, string>()
        {
            {"{UserName}", firstName + " " + lastName},
            {"{BookTitle}", bookTitle},
            {"{DueDate}", dueDate.ToString("MMMM dd, yyyy")},
            {"{Library Contact Information}", _options.SenderEmail }
        };
        var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "BorrowBookTemplate.html", keyValues);
        await _emailSender.SendEmailAsync(email!, "Borrowed Book Notification", emailBody);
    }
}
