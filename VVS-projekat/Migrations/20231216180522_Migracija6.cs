using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VVS_projekat.Migrations
{
    public partial class Migracija6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationPayment",
                table: "ReservationPayment");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReservationPayment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "MembershipPayment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationPayment",
                table: "ReservationPayment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPayment_PaymentId",
                table: "ReservationPayment",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationPayment",
                table: "ReservationPayment");

            migrationBuilder.DropIndex(
                name: "IX_ReservationPayment_PaymentId",
                table: "ReservationPayment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReservationPayment");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "MembershipPayment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationPayment",
                table: "ReservationPayment",
                column: "PaymentId");
        }
    }
}
