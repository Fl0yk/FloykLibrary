using AutoMapper;
using FloykLibrary.Application.Authors.Commands.CreateAuthor;
using FloykLibrary.Application.Shared.Mapper.AuthorMapping;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Moq;

namespace FloykLibrary.Tests.Authors.Commands
{
    public class CreateAuthorCommandHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandlerTests()
        {
            _authorRepositoryMock = new();
            _unitOfWorkMock = new();

            MapperConfiguration conf = new MapperConfiguration(config =>
            {
                config.AddProfile<CreateAuthorCommandToAuthor>();
            });
            _mapper = conf.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_CallCreateAuthor()
        {
            //Arrange
            Guid id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80");

            CreateAuthorCommand command = new() { Name = "Test", Surname = "Test", Country = "Test", DateOfBirth = DateTime.Parse("2024-10-10") };

            _authorRepositoryMock.Setup(
                    r => r.CreateAsync(
                        It.IsNotNull<Author>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(id));

            CreateAuthorCommandHandler handler = new(_authorRepositoryMock.Object, _unitOfWorkMock.Object, _mapper);

            //Act
            Guid result = await handler.Handle(command, default);

            //Assert
            _authorRepositoryMock.Verify(
                r => r.CreateAsync(
                    It.Is<Author>(a => a.Name == command.Name
                                    && a.Surname == command.Surname
                                    && a.DateOfBirth == command.DateOfBirth), 
                    It.IsAny<CancellationToken>()), 
                Times.Once);

            Assert.Equal(id, result);
        }
    }
}
