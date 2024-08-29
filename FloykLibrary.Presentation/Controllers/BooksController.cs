using FloykLibrary.Application.Books.Commands.AddImage;
using FloykLibrary.Application.Books.Commands.CreateBook;
using FloykLibrary.Application.Books.Commands.DeleteBook;
using FloykLibrary.Application.Books.Commands.TakeBook;
using FloykLibrary.Application.Books.Commands.UpdateBook;
using FloykLibrary.Application.Books.Queries.GetBookById;
using FloykLibrary.Application.Books.Queries.GetBookByISBN;
using FloykLibrary.Application.Books.Queries.GetBooksWithPagination;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> GetBooksWithPaginationAsync([FromQuery] int pageNumber = 1, 
                                                                [FromQuery] int pageSize = 3, 
                                                                CancellationToken token = default)
        {
            var books = await _mediator.Send(new GetBooksWithPaginationQuery(pageSize, pageNumber), token);

            return Ok(books);
        }

        [HttpGet("id/{id:guid}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute] Guid id, CancellationToken token)
        {
            BookDTO book = await _mediator.Send(new GetBookByIdQuery() { Id = id}, token);

            return Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetBookByIsbnAsync([FromRoute] string isbn, CancellationToken token)
        {
            BookDTO book = await _mediator.Send(new GetBookByIsbnQuery() { ISBN = isbn}, token);

            return Ok(book);
        }

        [HttpPost]
        [Authorize(Policy = PolicyTypes.AdminPolicy)]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookCommand createBookCommand, CancellationToken token)
        {
            var id = await _mediator.Send(createBookCommand, token);

            return Ok(id);
        }

        [HttpPost("image/{id:guid}")]
        [Authorize(Policy = PolicyTypes.AdminPolicy)]
        public async Task<IActionResult> AddImageAsync([FromRoute] Guid id, IFormFile formFile, CancellationToken token)
        {
            await _mediator.Send(new AddImageCommand() 
            { 
                Id = id, 
                FileName = formFile.FileName, 
                ImageStream = formFile.OpenReadStream() 
            }, token);

            return NoContent();
        }

        [HttpPost("take/{bookId:guid}/{returning:datetime}")]
        [Authorize(Policy = PolicyTypes.ClientPolicy)]
        public async Task<IActionResult> TakeBookAsync([FromRoute] Guid bookId, [FromRoute] DateTime returning, CancellationToken token)
        {
            string? UserId = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (UserId is null || !Guid.TryParse(UserId, out Guid guidId))
                return Unauthorized();

            await _mediator.Send(new TakeBookCommand() { UserId = guidId, BookId = bookId, ReturningBook = returning }, token);

            return NoContent();
        }

        [HttpPut]
        [Authorize(Policy = PolicyTypes.AdminPolicy)]
        public async Task<IActionResult> UpdateBookAsync([FromBody] UpdateBookCommand updateBookCommand, CancellationToken token)
        {
            await _mediator.Send(updateBookCommand, token);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = PolicyTypes.AdminPolicy)]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] Guid id, CancellationToken token)
        {
            await _mediator.Send(new DeleteBookCommand() { Id = id }, token);

            return NoContent();
        }
    }
}
