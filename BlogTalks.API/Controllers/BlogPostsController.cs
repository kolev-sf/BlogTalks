using BlogTalks.Application.BlogPost.Commands;
using BlogTalks.Application.BlogPost.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BlogPostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogPostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<GetAllResponse>> Get([FromQuery] GetAllRequest request)
    {
        var list = _mediator.Send(request);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public ActionResult Get([FromRoute] int id)
    {
        var response = _mediator.Send(new GetByIdRequest { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] CreateRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public OkResult Put(int id, [FromBody] object value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public OkResult Delete(int id)
    {
        return Ok();
    }
}