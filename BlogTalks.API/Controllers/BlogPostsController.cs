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
    public ActionResult<List<GetAllResponse>> Get()
    {
        var list = _mediator.Send(new GetAllRequest());
        return Ok(list);
    }

    [HttpGet("{id}")]
    public ActionResult Get([FromRoute] int id)
    {
        var response = _mediator.Send(new GetByIdRequest { Id = id });
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] CreateRequest request)
    {
        var response = await _mediator.Send(request);
        if(response == null)
        {
            return BadRequest("User not authorized");
        }
        return Ok(response);
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}