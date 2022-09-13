using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class latestnewmapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mapping");

            migrationBuilder.CreateTable(
                name: "AuthorshipRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorshipRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthorShips",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    AuthorshipRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthorShips", x => new { x.BookId, x.AuthorId, x.AuthorshipRoleId });
                    table.ForeignKey(
                        name: "FK_BookAuthorShips_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthorShips_AuthorshipRoles_AuthorshipRoleId",
                        column: x => x.AuthorshipRoleId,
                        principalTable: "AuthorshipRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthorShips_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthorShips_AuthorId",
                table: "BookAuthorShips",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthorShips_AuthorshipRoleId",
                table: "BookAuthorShips",
                column: "AuthorshipRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthorShips");

            migrationBuilder.DropTable(
                name: "AuthorshipRoles");

            migrationBuilder.CreateTable(
                name: "Mapping",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapping", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_Mapping_Author_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mapping_Book_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_BooksId",
                table: "Mapping",
                column: "BooksId");
        }
    }
}
