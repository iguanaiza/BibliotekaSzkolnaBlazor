using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotekaSzkolnaBlazor.Migrations
{
    /// <inheritdoc />
    public partial class AktualizacjaKoszyka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "BookReservationCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "BookReservationCarts");
        }
    }
}
