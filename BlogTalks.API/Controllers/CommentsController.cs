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

    [HttpGet("/blogPosts/{blogPostId}/comments")]
    public ActionResult GetByBlogPostId([FromRoute] int blogPostId)
    {
        var response = _mediator.Send(new GetByBlogPostIdRequest(blogPostId));
        return Ok(response);
    }

}