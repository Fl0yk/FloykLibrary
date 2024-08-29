using FloykLibrary.Application.Authors.Commands.DeleteAuthor;
using FloykLibrary.Application.Authors.Commands.UpdateAuthor;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace FloykLibrary.Tests.Authors.Commands
{
    public class DeleteAuthorCommandHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteAuthorCommandHandlerTests()
        {
            _authorRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public void Handle_Should_ThrowKeyNotFoundEx_WhenAuthorNotFound()
        {
            //Arrange
            Guid id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80");

            DeleteAuthorCommand command = new() { Id = id };

            DeleteAuthorCommandHandler handler = new(_authorRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act
            Func<Task> action = () => handler.Handle(command, default);

            //Assert
            Assert.ThrowsAsync<KeyNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_Should_CallDeleteeAuthor()
        {
            //Arrange
            Guid id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80");

            DeleteAuthorCommand command = new() { Id = id };

            _authorRepositoryMock.Setup(
                r => r.FirstOrDefaultAsync(
                        It.IsAny<Expression<Func<Author, bool>>>(),
                        It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Author()
                {
                    Id = id,
                    Name = "Test",
                    Surname = "Test",
                    Country = "Test",
                    DateOfBirth = DateTime.Parse("2024-10-10")
                })!);

            DeleteAuthorCommandHandler handler = new(_authorRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act
            await handler.Handle(command, default);

            //Assert
            _authorRepositoryMock.Verify(
                r => r.DeleteAsync(
                    It.Is<Author>(a => a.Id == id),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
