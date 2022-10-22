using FoxPhonebook.Application.Common.Models;
using FoxPhonebook.Application.Contacts.Commands.CreateContact;
using FoxPhonebook.Application.Contacts.Commands.RemoveContact;
using FoxPhonebook.Application.Contacts.Commands.UpdateContact;
using FoxPhonebook.Application.Contacts.Queries.GetContact;
using FoxPhonebook.Application.Contacts.Queries.GetContactList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoxPhonebook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<PaginatedList<GetContactListDto>>> GetContactList([FromQuery] GetContactListQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetContactDto>> GetContact([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetContactQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<Guid>> CreateContact([FromBody] CreateContactCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPut("")]
        public async Task<ActionResult<Guid>> UpdateContact([FromBody] UpdateContactCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveContact([FromRoute] Guid id)
        {
            var cmd = new RemoveContactCommand(id);
            var result = await _mediator.Send(cmd);
            return Ok();
        }
    }
}
