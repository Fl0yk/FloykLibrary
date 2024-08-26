using FloykLibrary.Application.Authors.Commands.CreateAuthor;
using FloykLibrary.Application.Authors.Commands.DeleteAuthor;
using FloykLibrary.Application.Authors.Commands.UpdateAuthor;
using FloykLibrary.Application.Authors.Queries.GetAuthorById;
using FloykLibrary.Application.Authors.Queries.GetAuthorsWithPagination;
using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FloykLibrary.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsWithPaginationAsync([FromQuery] int pageNumber = 1, 
                                                                       [FromQuery] int pageSise = 3, 
                                                                       CancellationToken token = default)
        {
            var authors = await _mediator.Send(new GetAuthorsWithPaginationQuery(pageSise, pageNumber), token);

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorByIdAsync([FromRoute] Guid id, CancellationToken token)
        {
            AuthorDTO author = await _mediator.Send(new GetAuthorByIdQuery() { Id = id }, token);

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAuthorCommand createAuthorCommand, CancellationToken token)
        {
            var id = await _mediator.Send(createAuthorCommand, token);

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthorAsync([FromBody] UpdateAuthorCommand updateAuthorCommand, CancellationToken token)
        {
            await _mediator.Send(updateAuthorCommand, token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] Guid id, CancellationToken token)
        {
            await _mediator.Send(new DeleteAuthorCommand() { Id = id }, token);

            return NoContent();
        }
    }
}
