﻿using FloykLibrary.Domain.Abstractions;
using FluentValidation;

namespace FloykLibrary.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(b => b.ISBN).NotEmpty()
                .Must(CheckLength)
                    .WithMessage("'{PropertyName}' must be equal 10 or 13")
                .MustAsync(CheckIsbnAsync)
                    .WithMessage("'{PropertyName}' must be unique");

            RuleFor(b => b.Title).NotEmpty();

            RuleFor(b => b.Description).NotEmpty();

            RuleFor(b => b.Genre).NotEmpty();

            RuleFor(b => b.AuthorsId).NotEmpty();
        }

        private static bool CheckLength(CreateBookCommand command, string isbn)
        {
            if (isbn.Length == 10 || isbn.Length == 13)
                return true;

            return false;
        }

        private Task<bool> CheckIsbnAsync(CreateBookCommand command, string isbn, CancellationToken token)
        {
            return _bookRepository.IsIsbnUniqueAsync(isbn, token);
        }
    }
}
