namespace Library.Web.HealthChecks;

public class MailHealthChecks(IOptions<EmailOptions> emailOptions) : IHealthCheck
{
    private readonly EmailOptions _emailOptions = emailOptions.Value;
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host: _emailOptions.SmtpServer, port: _emailOptions.SmtpPort, SecureSocketOptions.StartTls, cancellationToken: cancellationToken);
            await smtp.AuthenticateAsync(_emailOptions.SenderEmail, _emailOptions.Password, cancellationToken);
            return await Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception ex)
        {
            return await Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
        }
    }
}
