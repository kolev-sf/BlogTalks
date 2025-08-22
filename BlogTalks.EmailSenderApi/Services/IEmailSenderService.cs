using BlogTalks.EmailSenderAPI.Models;

namespace BlogTalks.EmailSenderAPI.Services;

public interface IEmailSenderService
{
    void SendEmail(string email);

    public Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent);

}