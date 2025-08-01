using BlogTalks.Application.BlogPost.Commands;
using BlogTalks.Application.BlogPost.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogPostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BlogPosts>
        [HttpGet]
        public ActionResult<List<GetAllResponse>> Get()
        {
            var list = _mediator.Send(new GetAllRequest());
            return Ok(list);
        }

        // GET api/<BlogPosts>/5
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

        // POST api/<BlogPosts>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CreateRequest request)
        {
            var response = await _mediator.Send(request);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response.Id);
        }

        // PUT api/<BlogPosts>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogPosts>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
