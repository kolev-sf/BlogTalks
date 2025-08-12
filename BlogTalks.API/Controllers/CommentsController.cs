using BlogTalks.Application.Comment.Commands;
using BlogTalks.Application.Comment.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
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
        if (response == null)
        {
            return BadRequest("BlogPost not found");
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

    [HttpGet("/blogPosts/{blogPostId}/comments")]
    public ActionResult GetByBlogPostId([FromRoute] int blogPostId)
    {
        var response = _mediator.Send(new GetByBlogPostIdRequest(blogPostId));
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

}