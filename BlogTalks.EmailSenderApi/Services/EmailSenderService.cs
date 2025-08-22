
namespace BlogTalks.EmailSenderAPI.Services;

public class EmailSenderService : IEmailSenderService
{
    public void SendEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
    {
    }
}