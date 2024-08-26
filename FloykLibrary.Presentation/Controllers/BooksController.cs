﻿using FloykLibrary.Application.Books.Commands.CreateBook;
using FloykLibrary.Application.Books.Commands.DeleteBook;
using FloykLibrary.Application.Books.Commands.UpdateBook;
using FloykLibrary.Application.Books.Queries.GetBookById;
using FloykLibrary.Application.Books.Queries.GetBookByISBN;
using FloykLibrary.Application.Books.Queries.GetBooksWithPagination;
using FloykLibrary.Application.Shared.Models.DTOs;
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
        public async Task<IActionResult> GetBooksWithPaginationAsync([FromQuery] int pageNumber = 1, 
                                                                [FromQuery] int pageSise = 3, 
                                                                CancellationToken token = default)
        {
            var books = await _mediator.Send(new GetBooksWithPaginationQuery(pageSise, pageNumber), token);

            return Ok(books);
        }

        [HttpGet("id/{id}")]
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
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookCommand createBookCommand, CancellationToken token)
        {
            var id = await _mediator.Send(createBookCommand, token);

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync([FromBody] UpdateBookCommand updateBookCommand, CancellationToken token)
        {
            await _mediator.Send(updateBookCommand, token);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookAsync([FromBody] DeleteBookCommand deleteBookCommand, CancellationToken token)
        {
            await _mediator.Send(deleteBookCommand, token);

            return NoContent();
        }
    }
}
