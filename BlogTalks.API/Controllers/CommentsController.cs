using BlogTalks.Application.Comment.Queries;
using BlogTalks.Application.Comment.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<Comments>
        //[HttpGet]
        //public ActionResult<List<GetAllResponse>> Get()
        //{
        //    var list = _mediator.Send(new GetAllRequest());
        //    return Ok(list);
        //}

        // GET api/<Comments>/5
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

        // POST api/<Comments>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CreateRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT api/<Comments>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Comments>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/<Comments>/5
        [HttpGet("/api/BlogPosts/{blogPostId}/comments")]
        public ActionResult GetByBlogpostId([FromRoute] int blogPostId)
        {
            var request = new GetByBlogPostIdRequest(blogPostId);
            var response = _mediator.Send(request);
            return Ok(response);
        }
    }
}
