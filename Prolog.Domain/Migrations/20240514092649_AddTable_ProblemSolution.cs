using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_ProblemSolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_driver_transport_driver_driver_driver_id",
                table: "DriverTransportDriver");

            migrationBuilder.DropForeignKey(
                name: "fk_driver_transport_driver_transports_transport_id",
                table: "DriverTransportDriver");

            migrationBuilder.DropPrimaryKey(
                name: "pk_driver_transport_driver",
                table: "DriverTransportDriver");

            migrationBuilder.DropColumn(
                name: "index",
                table: "order");

            migrationBuilder.RenameTable(
                name: "DriverTransportDriver",
                newName: "driver_transport_bind");

            migrationBuilder.RenameIndex(
                name: "ix_driver_transport_driver_transport_id",
                table: "driver_transport_bind",
                newName: "ix_driver_transport_bind_transport_id");

            migrationBuilder.RenameIndex(
                name: "ix_driver_transport_driver_driver_id",
                table: "driver_transport_bind",
                newName: "ix_driver_transport_bind_driver_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_driver_transport_bind",
                table: "driver_transport_bind",
                column: "id");

            migrationBuilder.CreateTable(
                name: "problem_solution",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    stop_type = table.Column<int>(type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    longitude = table.Column<string>(type: "text", nullable: false),
                    latitude = table.Column<string>(type: "text", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_problem_solution", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_driver_transport_bind_driver_driver_id",
                table: "driver_transport_bind",
                column: "driver_id",
                principalTable: "driver",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_driver_transport_bind_transports_transport_id",
                table: "driver_transport_bind",
                column: "transport_id",
                principalTable: "transport",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_driver_transport_bind_driver_driver_id",
                table: "driver_transport_bind");

            migrationBuilder.DropForeignKey(
                name: "fk_driver_transport_bind_transports_transport_id",
                table: "driver_transport_bind");

            migrationBuilder.DropTable(
                name: "problem_solution");

            migrationBuilder.DropPrimaryKey(
                name: "pk_driver_transport_bind",
                table: "driver_transport_bind");

            migrationBuilder.RenameTable(
                name: "driver_transport_bind",
                newName: "DriverTransportDriver");

            migrationBuilder.RenameIndex(
                name: "ix_driver_transport_bind_transport_id",
                table: "DriverTransportDriver",
                newName: "ix_driver_transport_driver_transport_id");

            migrationBuilder.RenameIndex(
                name: "ix_driver_transport_bind_driver_id",
                table: "DriverTransportDriver",
                newName: "ix_driver_transport_driver_driver_id");

            migrationBuilder.AddColumn<long>(
                name: "index",
                table: "order",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_driver_transport_driver",
                table: "DriverTransportDriver",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_driver_transport_driver_driver_driver_id",
                table: "DriverTransportDriver",
                column: "driver_id",
                principalTable: "driver",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_driver_transport_driver_transports_transport_id",
                table: "DriverTransportDriver",
                column: "transport_id",
                principalTable: "transport",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
