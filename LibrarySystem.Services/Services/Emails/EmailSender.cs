namespace LibrarySystem.Services.Services.Emails
{
    public class EmailSender(IOptions<EmailOptions> emailOptions) : IEmailSender
    {
        private readonly EmailOptions _emailOptions = emailOptions.Value;

        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailOptions.SenderName, _emailOptions.SenderEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody=message };
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host: _emailOptions.SmtpServer, port: _emailOptions.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailOptions.SenderEmail, _emailOptions.Password);
            await smtp.SendAsync(mimeMessage);
            await smtp.DisconnectAsync(true);
        }
    }
}
