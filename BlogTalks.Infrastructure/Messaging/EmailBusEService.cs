using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogTalks.Application.Contracts;

namespace BlogTalks.Infrastructure.Messaging;

public class EmailBusEService : IEmailService
{
    public Task Send(EmailToSendModel model)
    {
        throw new NotImplementedException();
    }
}