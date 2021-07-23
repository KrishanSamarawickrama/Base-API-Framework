using System.Threading.Tasks;
using Base.Application;
using Base.Application.Commands;
using Base.Application.Common;
using Base.Application.Dto;
using Base.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        public PersonController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("GetAll")]
        [Produces(typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromBody] PaginationAndSort dto)
        {
            if (dto == null) dto = new();
            var result = await this.mediator.Send(new GetPersonsQuery(dto));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces(typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await mediator.Send(new GetPersonByIdQuery(id));
            return Ok(result);
        }

        [HttpPost(Name = nameof(Post))]
        [Produces(typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] AddPersonDto dto)
        {
            var result = await this.mediator.Send(new AddPersonCommand(dto));
            return Ok(result);
        }

        [HttpPut("{id}", Name = nameof(Put))]
        [Produces(typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(string id, [FromBody] AddPersonDto dto)
        {
            var result = await mediator.Send(new EditPersonCommand(id, dto));
            return Ok(result);
        }

        [HttpDelete("{id}", Name = nameof(Delete))]
        [Produces(typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await mediator.Send(new DeletePersonCommand(id));
            return Ok(result);
        }
    }
}