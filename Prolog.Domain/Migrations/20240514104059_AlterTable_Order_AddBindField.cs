using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_Order_AddBindField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_driver_driver_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_transports_transport_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "ix_order_driver_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "driver_id",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "transport_id",
                table: "order",
                newName: "driver_transport_bind_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_transport_id",
                table: "order",
                newName: "ix_order_driver_transport_bind_id");

            migrationBuilder.AddColumn<decimal>(
                name: "distance",
                table: "driver_transport_bind",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "end_date",
                table: "driver_transport_bind",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "orders_completed_count",
                table: "driver_transport_bind",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "start_date",
                table: "driver_transport_bind",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "total_orders_count",
                table: "driver_transport_bind",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_order_driver_transport_bind_driver_transport_bind_id",
                table: "order",
                column: "driver_transport_bind_id",
                principalTable: "driver_transport_bind",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_driver_transport_bind_driver_transport_bind_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "distance",
                table: "driver_transport_bind");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "driver_transport_bind");

            migrationBuilder.DropColumn(
                name: "orders_completed_count",
                table: "driver_transport_bind");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "driver_transport_bind");

            migrationBuilder.DropColumn(
                name: "total_orders_count",
                table: "driver_transport_bind");

            migrationBuilder.RenameColumn(
                name: "driver_transport_bind_id",
                table: "order",
                newName: "transport_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_driver_transport_bind_id",
                table: "order",
                newName: "ix_order_transport_id");

            migrationBuilder.AddColumn<Guid>(
                name: "driver_id",
                table: "order",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_driver_id",
                table: "order",
                column: "driver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_driver_driver_id",
                table: "order",
                column: "driver_id",
                principalTable: "driver",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_order_transports_transport_id",
                table: "order",
                column: "transport_id",
                principalTable: "transport",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
