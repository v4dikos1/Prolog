using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class alterTable_Drivers_RemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "driver");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "driver",
                newName: "telegram");

            migrationBuilder.AddColumn<decimal>(
                name: "salary",
                table: "driver",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salary",
                table: "driver");

            migrationBuilder.RenameColumn(
                name: "telegram",
                table: "driver",
                newName: "email");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "driver",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
