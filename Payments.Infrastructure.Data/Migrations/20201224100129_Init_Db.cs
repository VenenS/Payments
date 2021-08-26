using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Payments.Infrastructure.Data.Migrations
{
    public partial class Init_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ip_allowlist_address",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    authorized_ip_address = table.Column<string>(nullable: false),
                    ip_with = table.Column<int>(nullable: false),
                    ip_before = table.Column<int>(nullable: false),
                    payment_system_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ip_allowlist_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log_message",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    severity = table.Column<string>(nullable: true),
                    msg_code = table.Column<string>(nullable: true),
                    msg_text = table.Column<string>(nullable: true),
                    source_package = table.Column<string>(nullable: true),
                    msg_datetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_message", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_system",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_system", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    SecretKey = table.Column<string>(nullable: false),
                    NotificationUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: DateTime.Now),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    UserRequestId = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    PaymentSystemId = table.Column<long>(nullable: false),
                    PaymentSystemOrderId = table.Column<string>(nullable: true),
                    notification_status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_payment_system_PaymentSystemId",
                        column: x => x.PaymentSystemId,
                        principalTable: "payment_system",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification_queue",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_time_create = table.Column<DateTime>(nullable: false, defaultValue:DateTime.Now),
                    date_time_update = table.Column<DateTime>(nullable: false),
                    count = table.Column<int>(nullable: false, defaultValue: 0),
                    id_payment = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_queue", x => x.id);
                    table.ForeignKey(
                        name: "FK_notification_queue_payment_id_payment",
                        column: x => x.id_payment,
                        principalTable: "payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_info",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    payment_id = table.Column<long>(nullable: false),
                    request = table.Column<string>(nullable: true),
                    response = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_info_payment_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "payment_system",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 1L, new DateTime(2020, 12, 24, 13, 1, 29, 448, DateTimeKind.Local).AddTicks(3788), "Yandex", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "Name", "NotificationUrl", "SecretKey" },
                values: new object[,]
                {
                    { 1L, "Education", "", "XE8tiBF2wfG3B6gj2LkPjNxgbO3IJWDYfWq5tCCAXTiy5R2sqQNVOPxa2ZFVtdDy" },
                    { 2L, "Food", "", "XE8tiBF2wfG3B6gj2LkPjNxgbO3IJWDYfWq5tCCAXTiy5R2sqQNVOPxa2ZFVtdD3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_notification_queue_id_payment",
                table: "notification_queue",
                column: "id_payment",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_PaymentSystemId",
                table: "payment",
                column: "PaymentSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_UserId",
                table: "payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_info_payment_id",
                table: "payment_info",
                column: "payment_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ip_allowlist_address");

            migrationBuilder.DropTable(
                name: "log_message");

            migrationBuilder.DropTable(
                name: "notification_queue");

            migrationBuilder.DropTable(
                name: "payment_info");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "payment_system");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
