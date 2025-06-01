using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotekaSzkolnaBlazor.Migrations
{
    /// <inheritdoc />
    public partial class dodanieTabelSpecialTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookSpecialTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSpecialTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooksBookSpecialTags",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookSpecialTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksBookSpecialTags", x => new { x.BookId, x.BookSpecialTagId });
                    table.ForeignKey(
                        name: "FK_BooksBookSpecialTags_BookSpecialTags_BookSpecialTagId",
                        column: x => x.BookSpecialTagId,
                        principalTable: "BookSpecialTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksBookSpecialTags_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksBookSpecialTags_BookSpecialTagId",
                table: "BooksBookSpecialTags",
                column: "BookSpecialTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksBookSpecialTags");

            migrationBuilder.DropTable(
                name: "BookSpecialTags");
        }
    }
}
