using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_DriverTransportBind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "index",
                table: "order",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverTransportDriver",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    transport_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    problem_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_driver_transport_driver", x => x.id);
                    table.ForeignKey(
                        name: "fk_driver_transport_driver_driver_driver_id",
                        column: x => x.driver_id,
                        principalTable: "driver",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_driver_transport_driver_transports_transport_id",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_driver_transport_driver_driver_id",
                table: "DriverTransportDriver",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_driver_transport_driver_transport_id",
                table: "DriverTransportDriver",
                column: "transport_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverTransportDriver");

            migrationBuilder.DropColumn(
                name: "index",
                table: "order");
        }
    }
}
