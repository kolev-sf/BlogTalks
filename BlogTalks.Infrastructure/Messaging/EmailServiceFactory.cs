using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTalks.Infrastructure.Messaging;

public class EmailServiceFactory : IEmailServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public EmailServiceFactory(IServiceProvider serviceProvider, IEmailService emailService)
    {
        _serviceProvider = serviceProvider;
    }

    public IEmailService Create(string registration)
    {
        return registration switch
        {
            "EmailHttpService" => _serviceProvider.GetRequiredKeyedService<EmailHttpService>("EmailHttpService"),
            "EmailBusEService" => _serviceProvider.GetRequiredKeyedService<EmailBusEService>("EmailBusEService"),
            _ => throw new NotSupportedException("Unsupported email service")
        };
    }
}

public interface IEmailServiceFactory
{
    IEmailService Create(string registration);
}