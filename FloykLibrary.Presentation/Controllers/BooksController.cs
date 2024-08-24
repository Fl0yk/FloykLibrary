using FloykLibrary.Application.Books.Commands.CreateBook;
using FloykLibrary.Application.Books.Queries.GetBooksWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FloykLibrary.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSise = 3, CancellationToken token = default)
        {
            var books = await _mediator.Send(new GetBooksWithPaginationQuery(pageSise, pageNumber), token);

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookCommand createBookCommand, CancellationToken token)
        {
            var id = await _mediator.Send(createBookCommand, token);

            return Ok(id);
        }
    }
}
