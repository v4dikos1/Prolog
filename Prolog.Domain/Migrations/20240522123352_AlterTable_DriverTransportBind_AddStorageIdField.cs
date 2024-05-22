using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_DriverTransportBind_AddStorageIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "storage_id",
                table: "driver_transport_bind",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_driver_transport_bind_storage_id",
                table: "driver_transport_bind",
                column: "storage_id");

            migrationBuilder.AddForeignKey(
                name: "fk_driver_transport_bind_storages_storage_id",
                table: "driver_transport_bind",
                column: "storage_id",
                principalTable: "storage",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_driver_transport_bind_storages_storage_id",
                table: "driver_transport_bind");

            migrationBuilder.DropIndex(
                name: "ix_driver_transport_bind_storage_id",
                table: "driver_transport_bind");

            migrationBuilder.DropColumn(
                name: "storage_id",
                table: "driver_transport_bind");
        }
    }
}
