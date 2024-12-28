using LibrarySystem.Services.Services.Fines;
using Newtonsoft.Json.Linq;

namespace LibrarySystem.Services.Services.Notifications;
public class BorrowedBookNotificationServices(IUnitOfWork unitOfWork,
    IEmailSender emailSender,
    IOptions<EmailOptions> options,IFineNotificationServices fineServices) : IBorrowedBookNotificationServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly EmailOptions _options = options.Value;
    private readonly IFineNotificationServices _fineServices = fineServices;
    public async Task SendFineNotificationToBorrower()
    {
        var fines=await _unitOfWork.FineRepository.GetAllWithUserAndBookAsync(x => !x.IsPaid);
        foreach (var fine in fines)
        {
            await SendFineNotificationEmail(fine.FirstName, fine.LastName, fine.Email,
                fine.BooksTitle, fine.DueAt, fine.Amount, fine.TotalAmount);
        }
    }
    public async Task SendReminderNotificationToBorrower()
    {
        var tomorrow = DateTime.Now.AddDays(1).Date;
        var borrowedBooks = await _unitOfWork.BorrowedBookRepository
            .GetAllWithUserAndBook(x => x.DueDate.Date <tomorrow&& x.ReturnDate == null);

        foreach (var borrowedBook in borrowedBooks)
        {
            await SendReminderNotificationEmail(borrowedBook.FirstName,
                borrowedBook.LastName, borrowedBook.Email, borrowedBook.BookTitle, borrowedBook.DueDate);
        }
    }


    private async Task SendFineNotificationEmail(string firstName,string lastName,string email
        , List<string> booksTitle, DateTime dueDate,decimal fineAmount,decimal totalFineAmount)
    {
        var keyValues = new Dictionary<string, string>()
    {
        {"{UserName}", firstName + " " + lastName},
        {"{BookTitle}", string.Join(',',booksTitle.Select(x=>x))},
        {"{DueDate}", dueDate.ToString("MMMM dd, yyyy")},
        {"{FineAmount}", fineAmount.ToString("C")},
        {"{TotalFineAmount}", totalFineAmount.ToString("C")},
        {"{LibraryContactInformation}", _options.SenderEmail }
    };
        var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "BorrowBookTemplate.html", keyValues);
        await _emailSender.SendEmailAsync(email!, "Fines Of Borrowed Book Notification", emailBody);
    }
    private async Task SendReminderNotificationEmail(string firstName, string lastName, string email, string bookTitle, DateTime dueDate)
    {
        var keyValues = new Dictionary<string, string>()
        {
            {"{UserName}", firstName + " " + lastName},
            {"{BookTitle}", bookTitle},
            {"{DueDate}", dueDate.ToString("MMMM dd, yyyy")},
            {"{LibraryContactInformation}", _options.SenderEmail }
        };
        var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "BorrowedBookReminderTemplate.html", keyValues);
        await _emailSender.SendEmailAsync(email!, "Reminder Borrowed Book Notification", emailBody);
    }
}
