using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VVS_projekat.Migrations
{
    public partial class Migracija5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "ReservationPayment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "ReservationPayment");
        }
    }
}
