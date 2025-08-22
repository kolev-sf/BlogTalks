using BlogTalks.Application.Contracts;
using Microsoft.VisualBasic;

namespace BlogTalks.Infrastructure.Messaging;

public interface IEmailService
{
    Task Send(EmailToSendModel model);
}