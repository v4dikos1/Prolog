using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class alterTable_Customer_RemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "patronymic",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "customer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "customer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                table: "customer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "customer",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
