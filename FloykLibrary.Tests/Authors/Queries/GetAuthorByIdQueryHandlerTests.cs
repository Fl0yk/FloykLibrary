using AutoMapper;
using FloykLibrary.Application.Authors.Queries.GetAuthorById;
using FloykLibrary.Application.Shared.Mapper.AuthorMapping;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace FloykLibrary.Tests.Authors.Queries
{
    public class GetAuthorByIdQueryHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryHandlerTests()
        {
            _authorRepositoryMock = new();

            MapperConfiguration conf = new MapperConfiguration(config =>
            {
                config.AddProfile<AuthorToDTO>();
            });
            _mapper = conf.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_ThrowKeyNotFoundEx_WhenAuthorNotFound()
        {
            //Arrange
            Guid id = TestAuthor.Id;

            GetAuthorByIdQuery query = new() { Id = id };

            GetAuthorByIdQueryHandler handler = new(_authorRepositoryMock.Object, _mapper);

            //Act
            Func<Task> action = () => handler.Handle(query, default);

            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_Should_ReturnAuthor_WhenIdIsValid()
        {
            //Arrange
            Guid id = TestAuthor.Id;

            _authorRepositoryMock.Setup(
                r => r.FirstOrDefaultAsync(
                        It.IsAny<Expression<Func<Author, bool>>>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<Author, object>>>()))
                .Returns(Task.FromResult(TestAuthor)!);

            GetAuthorByIdQuery query = new() { Id = id };

            GetAuthorByIdQueryHandler handler = new(_authorRepositoryMock.Object, _mapper);

            //Act
            AuthorDTO result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);

            _authorRepositoryMock.Verify(
                r => r.FirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Author, bool>>>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<Expression<Func<Author, object>>>()),
                Times.Once);
        }

        private static Author TestAuthor => new()
        { 
            Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"), 
            Name = "Test", 
            Surname = "Test", 
            Country = "Test", 
            DateOfBirth = DateTime.Parse("2024-10-10")
        };
    }
}
