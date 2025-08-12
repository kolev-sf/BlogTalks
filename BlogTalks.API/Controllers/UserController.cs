using BlogTalks.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    //// GET: api/<BlogPosts>
    //[HttpGet]
    //public ActionResult<List<GetAllResponse>> Get()
    //{
    //    var list = _mediator.Send(new GetAllRequest());
    //    return Ok(list);
    //}

    // GET api/<BlogPosts>/5
    //[HttpGet("{id}")]
    //public ActionResult Get([FromRoute] int id)
    //{
    //    var response = _mediator.Send(new GetByIdRequest { Id = id });
    //    if (response == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(response);
    //}

    // POST api/<BlogPosts>
    [HttpPost("/register")]
    [AllowAnonymous]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await mediator.Send(request);
        if (response == null)
        {
            return Forbid();
        }
        return Ok(response);
    }

    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await mediator.Send(request);
        if (response == null)
        {
            return Unauthorized("Username and password does not match");
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