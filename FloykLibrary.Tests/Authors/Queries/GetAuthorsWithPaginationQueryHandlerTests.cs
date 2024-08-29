using AutoMapper;
using FloykLibrary.Application.Authors.Queries.GetAuthorsWithPagination;
using FloykLibrary.Application.Shared.Mapper.AuthorMapping;
using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace FloykLibrary.Tests.Authors.Queries
{
    public class GetAuthorsWithPaginationQueryHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly IMapper _mapper;

        public GetAuthorsWithPaginationQueryHandlerTests()
        {
            _authorRepositoryMock = new();

            MapperConfiguration conf = new MapperConfiguration(config =>
            {
                config.AddProfile<AuthorToDTO>();
            });
            _mapper = conf.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFirstPage_WhenPageNoEqual1()
        {
            //Arrange
            _authorRepositoryMock.Setup(
                r => r.GetAllAsync(
                        It.IsAny<Expression<Func<Author, bool>>>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<Author, object>>>()))
                .Returns(Task.FromResult(Authors.AsQueryable())!);

            int pageSize = 3;
            int pageNo = 1;

            GetAuthorsWithPaginationQuery query = new(pageSize, pageNo);

            GetAuthorsWithPaginationQueryHandler handler = new(_authorRepositoryMock.Object, _mapper);

            //Act
            PaginatedResult<AuthorDTO> result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.Equal(result.Items.Count(), result.PageSize);
            Assert.Equal(result.PageSize, pageSize);
        }

        [Fact]
        public async Task Handle_Should_ReturnSecondPage_WhenPageNoEqual2()
        {
            //Arrange
            _authorRepositoryMock.Setup(
                r => r.GetAllAsync(
                        It.IsAny<Expression<Func<Author, bool>>>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<Author, object>>>()))
                .Returns(Task.FromResult(Authors.AsQueryable())!);

            int pageSize = 3;
            int pageNo = 2;

            GetAuthorsWithPaginationQuery query = new(pageSize, pageNo);

            GetAuthorsWithPaginationQueryHandler handler = new(_authorRepositoryMock.Object, _mapper);

            //Act
            PaginatedResult<AuthorDTO> result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.Single(result.Items);
            Assert.Equal(result.PageSize, pageSize);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyItems_WhenPageNoMoreThanMaximum()
        {
            //Arrange
            _authorRepositoryMock.Setup(
                r => r.GetAllAsync(
                        It.IsAny<Expression<Func<Author, bool>>>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<Author, object>>>()))
                .Returns(Task.FromResult(Authors.AsQueryable())!);

            int pageSize = 3;
            int pageNo = 10;

            GetAuthorsWithPaginationQuery query = new(pageSize, pageNo);

            GetAuthorsWithPaginationQueryHandler handler = new(_authorRepositoryMock.Object, _mapper);

            //Act
            PaginatedResult<AuthorDTO> result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
        }

        private static Author[] Authors => [
            new()
            {
                Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 1",
                Surname = "Test 1",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2024-10-10")
            },
            new()
            {
                Id = Guid.Parse("7C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 2",
                Surname = "Test 2",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2024-10-10")
            },
            new()
            {
                Id = Guid.Parse("6C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 3",
                Surname = "Test 3",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2024-10-10")
            },
            new()
            {
                Id = Guid.Parse("5C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 4",
                Surname = "Test 4",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2024-10-10")
            },
            ];
    }
}
