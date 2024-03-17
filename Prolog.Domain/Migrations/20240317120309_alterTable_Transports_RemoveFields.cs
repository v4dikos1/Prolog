using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class alterTable_Transports_RemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "transport");

            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "transport",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brand",
                table: "transport");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "transport",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
