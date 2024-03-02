using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prolog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "action_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    filter = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    identity_user_id = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    user_surname = table.Column<string>(type: "text", nullable: false),
                    action_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    action_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_action_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "external_system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_external_system", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_system_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_external_systems_external_system_id",
                        column: x => x.external_system_id,
                        principalTable: "external_system",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "driver",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_system_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_driver", x => x.id);
                    table.ForeignKey(
                        name: "fk_driver_external_systems_external_system_id",
                        column: x => x.external_system_id,
                        principalTable: "external_system",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_system_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    weight = table.Column<decimal>(type: "numeric", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_external_system_external_system_id",
                        column: x => x.external_system_id,
                        principalTable: "external_system",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "storage",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_system_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    coordinates = table.Column<string>(type: "text", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storage", x => x.id);
                    table.ForeignKey(
                        name: "fk_storage_external_system_external_system_id",
                        column: x => x.external_system_id,
                        principalTable: "external_system",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transport",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_system_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false),
                    capacity = table.Column<decimal>(type: "numeric", nullable: false),
                    fuel_consumption = table.Column<decimal>(type: "numeric", nullable: false),
                    licence_plate = table.Column<string>(type: "text", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transport", x => x.id);
                    table.ForeignKey(
                        name: "fk_transport_external_system_external_system_id",
                        column: x => x.external_system_id,
                        principalTable: "external_system",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_system_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transport_id = table.Column<Guid>(type: "uuid", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    payment_type = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<int>(type: "integer", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_driver_driver_id",
                        column: x => x.driver_id,
                        principalTable: "driver",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_external_system_external_system_id",
                        column: x => x.external_system_id,
                        principalTable: "external_system",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_transports_transport_id",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    storage_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_item_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false),
                    date_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_item_order_item_order_item_id",
                        column: x => x.order_item_id,
                        principalTable: "order_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_product_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_product_item_storages_storage_id",
                        column: x => x.storage_id,
                        principalTable: "storage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_external_system_id",
                table: "customer",
                column: "external_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_driver_external_system_id",
                table: "driver",
                column: "external_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_external_system_identity_id",
                table: "external_system",
                column: "identity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_customer_id",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_driver_id",
                table: "order",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_external_system_id",
                table: "order",
                column: "external_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_transport_id",
                table: "order",
                column: "transport_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_product_item_id",
                table: "order_item",
                column: "product_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_external_system_id",
                table: "product",
                column: "external_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_order_item_id",
                table: "product_item",
                column: "order_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_item_product_id",
                table: "product_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_storage_id",
                table: "product_item",
                column: "storage_id");

            migrationBuilder.CreateIndex(
                name: "ix_storage_external_system_id",
                table: "storage",
                column: "external_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_transport_external_system_id",
                table: "transport",
                column: "external_system_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_product_items_product_item_id",
                table: "order_item",
                column: "product_item_id",
                principalTable: "product_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customer_external_systems_external_system_id",
                table: "customer");

            migrationBuilder.DropForeignKey(
                name: "fk_driver_external_systems_external_system_id",
                table: "driver");

            migrationBuilder.DropForeignKey(
                name: "fk_order_external_system_external_system_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_product_external_system_external_system_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_storage_external_system_external_system_id",
                table: "storage");

            migrationBuilder.DropForeignKey(
                name: "fk_transport_external_system_external_system_id",
                table: "transport");

            migrationBuilder.DropForeignKey(
                name: "fk_order_customer_customer_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_driver_driver_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_transports_transport_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_order_order_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_product_items_product_item_id",
                table: "order_item");

            migrationBuilder.DropTable(
                name: "action_log");

            migrationBuilder.DropTable(
                name: "external_system");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "driver");

            migrationBuilder.DropTable(
                name: "transport");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product_item");

            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "storage");
        }
    }
}
