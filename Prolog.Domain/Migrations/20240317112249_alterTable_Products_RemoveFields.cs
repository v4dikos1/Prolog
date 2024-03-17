using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class alterTable_Products_RemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "product");

            migrationBuilder.AddColumn<decimal>(
                name: "volume",
                table: "product",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "volume",
                table: "product");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "product",
                type: "text",
                nullable: true);
        }
    }
}
