using MediatR;

namespace BlogTalks.Application.BlogPost.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    public CreateHandler()
    {
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        // add insert logic here

        // return Id of created Comment Entity
        return new CreateResponse(5);
    }
}
