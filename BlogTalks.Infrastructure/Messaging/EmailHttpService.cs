using BlogTalks.Application.Contracts;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BlogTalks.Infrastructure.Messaging;

public class EmailHttpService : IEmailService
{
    private readonly ILogger<EmailHttpService> _logger;

    public EmailHttpService(ILogger<EmailHttpService> logger)
    {
        _logger = logger;
    }

    public async Task Send(EmailToSendModel model)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7055");

        var response = await httpClient.PostAsJsonAsync("/SendEmail", model);
        if (!response.IsSuccessStatusCode)
            _logger.LogError("Failed to send email. Status code: {StatusCode}, Reason: {ReasonPhrase}",
                response.StatusCode, response.ReasonPhrase);
    }
}