using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FloykLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TakingBook = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturningBook = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.UniqueConstraint("AK_Books_ISBN", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_Books_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Country", "DateOfBirth", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("33f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "Россия", new DateTime(1948, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Терри", "Пратчетт" },
                    { new Guid("34f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "Англия", new DateTime(1960, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Нил", "Гейман" },
                    { new Guid("35f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "Россия", new DateTime(1828, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лев", "Толстой" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "Genre", "ISBN", "Image", "ReturningBook", "TakingBook", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("23f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "Книга получила в целом положительные оценки критиков. Роман номинировался на Всемирную премию фэнтези и премию журнала «Локус».", "Роман", "9785041772932", null, null, null, "Благие знамения", null },
                    { new Guid("24f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "«Мертвые души» — гениальное произведение Николая Васильевича Гоголя, учебник жизни и ключ к пониманию характеров и типажей нашего общества. Сам автор определил жанр произведения как поэму.", "Поэма", "5170287402", null, null, null, "Мертвые души", null },
                    { new Guid("25f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "В книгу вошли первый и второй тома романа «Война и мир» – одного из самых знаменитых произведений литературы XIX века.", "Роман", "5170064004", null, null, null, "Война и мир. Книга 1", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "client" },
                    { 2, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("11f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), "admin@mail.ru", "Admin", null, null });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { new Guid("33f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), new Guid("23f010ed-8c38-4eeb-b9ec-5fb56ccf3189") },
                    { new Guid("34f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), new Guid("23f010ed-8c38-4eeb-b9ec-5fb56ccf3189") },
                    { new Guid("35f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), new Guid("24f010ed-8c38-4eeb-b9ec-5fb56ccf3189") },
                    { new Guid("35f010ed-8c38-4eeb-b9ec-5fb56ccf3189"), new Guid("25f010ed-8c38-4eeb-b9ec-5fb56ccf3189") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("11f010ed-8c38-4eeb-b9ec-5fb56ccf3189") },
                    { 2, new Guid("11f010ed-8c38-4eeb-b9ec-5fb56ccf3189") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
