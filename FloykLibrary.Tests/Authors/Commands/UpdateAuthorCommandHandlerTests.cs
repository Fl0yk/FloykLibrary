using AutoMapper;
using FloykLibrary.Application.Authors.Commands.UpdateAuthor;
using FloykLibrary.Application.Shared.Mapper.AuthorMapping;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace FloykLibrary.Tests.Authors.Commands
{
    public class UpdateAuthorCommandHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandlerTests()
        {
            _authorRepositoryMock = new();
            _unitOfWorkMock = new();

            MapperConfiguration conf = new MapperConfiguration(config =>
            {
                config.AddProfile<UpdateAuthorCommandToAuthor>();
            });
            _mapper = conf.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_ThrowKeyNotFoundEx_WhenAuthorNotFound()
        {
            //Arrange
            Guid id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80");

            UpdateAuthorCommand command = new() { Id = id, Name = "Test", Surname = "Test", Country = "Test", DateOfBirth = DateTime.Parse("2024-10-10") };

            UpdateAuthorCommandHandler handler = new(_authorRepositoryMock.Object, _unitOfWorkMock.Object, _mapper);

            //Act
            Func<Task> action = () => handler.Handle(command, default);

            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(action);
        }

        [Fact]
        public async Task Handle_Should_CallUpdateAuthor()
        {
            //Arrange
            Guid id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80");

            UpdateAuthorCommand command = new() { Id = id, Name = "Test", Surname = "Test", Country = "Test", DateOfBirth = DateTime.Parse("2024-10-10") };

            _authorRepositoryMock.Setup(
                    r => r.UpdateAsync(
                        It.IsNotNull<Author>(),
                        It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(id));

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

            UpdateAuthorCommandHandler handler = new(_authorRepositoryMock.Object, _unitOfWorkMock.Object, _mapper);

            //Act
            Guid result = await handler.Handle(command, default);

            //Assert
            _authorRepositoryMock.Verify(
                r => r.UpdateAsync(
                    It.Is<Author>(a => a.Name == command.Name
                                    && a.Surname == command.Surname
                                    && a.DateOfBirth == command.DateOfBirth),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            Assert.Equal(id, result);
        }
    }
}
