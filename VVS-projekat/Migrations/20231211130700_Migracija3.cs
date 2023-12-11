using Microsoft.EntityFrameworkCore.Migrations;

namespace VVS_projekat.Migrations
{
    public partial class Migracija3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Librarian_LibraryUser_Id",
                table: "Librarian");

            migrationBuilder.DropForeignKey(
                name: "FK_Librarian_LibraryUser_LibraryUserId",
                table: "Librarian");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMember_LibraryUser_Id",
                table: "LibraryMember");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryMember_LibraryUser_LibraryUserId",
                table: "LibraryMember");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipPayment_LibraryMember_LibraryMemberId",
                table: "MembershipPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_LibraryMember_LibraryMemberId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_LibraryMember_LibraryMemberId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "LibraryUser");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_LibraryMemberId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Rating_LibraryMemberId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_MembershipPayment_LibraryMemberId",
                table: "MembershipPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember");

            migrationBuilder.DropIndex(
                name: "IX_LibraryMember_LibraryUserId",
                table: "LibraryMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Librarian",
                table: "Librarian");

            migrationBuilder.DropIndex(
                name: "IX_Librarian_LibraryUserId",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "LibraryMemberId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "LibraryMemberId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "LibraryMemberId",
                table: "MembershipPayment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "LibraryUserId",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "LibraryUserId",
                table: "Librarian");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryMemberFk",
                table: "MembershipPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LibraryMemberId",
                table: "LibraryMember",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "LibraryMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "LibraryMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "LibraryMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryUserPassword",
                table: "LibraryMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryUsername",
                table: "LibraryMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LibrarianId",
                table: "Librarian",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Librarian",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Librarian",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Librarian",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryUserPassword",
                table: "Librarian",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryUsername",
                table: "Librarian",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember",
                column: "LibraryMemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Librarian",
                table: "Librarian",
                column: "LibrarianId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_LibraryMemberFk",
                table: "Reservation",
                column: "LibraryMemberFk");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_LibraryMemberFk",
                table: "Rating",
                column: "LibraryMemberFk");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPayment_LibraryMemberFk",
                table: "MembershipPayment",
                column: "LibraryMemberFk");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipPayment_LibraryMember_LibraryMemberFk",
                table: "MembershipPayment",
                column: "LibraryMemberFk",
                principalTable: "LibraryMember",
                principalColumn: "LibraryMemberId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_LibraryMember_LibraryMemberFk",
                table: "Rating",
                column: "LibraryMemberFk",
                principalTable: "LibraryMember",
                principalColumn: "LibraryMemberId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_LibraryMember_LibraryMemberFk",
                table: "Reservation",
                column: "LibraryMemberFk",
                principalTable: "LibraryMember",
                principalColumn: "LibraryMemberId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipPayment_LibraryMember_LibraryMemberFk",
                table: "MembershipPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_LibraryMember_LibraryMemberFk",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_LibraryMember_LibraryMemberFk",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_LibraryMemberFk",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Rating_LibraryMemberFk",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_MembershipPayment_LibraryMemberFk",
                table: "MembershipPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Librarian",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "LibraryMemberId",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "LibraryUserPassword",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "LibraryUsername",
                table: "LibraryMember");

            migrationBuilder.DropColumn(
                name: "LibrarianId",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "LibraryUserPassword",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "LibraryUsername",
                table: "Librarian");

            migrationBuilder.AddColumn<string>(
                name: "LibraryMemberId",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryMemberId",
                table: "Rating",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LibraryMemberFk",
                table: "MembershipPayment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryMemberId",
                table: "MembershipPayment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "LibraryMember",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LibraryUserId",
                table: "LibraryMember",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Librarian",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LibraryUserId",
                table: "Librarian",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryMember",
                table: "LibraryMember",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Librarian",
                table: "Librarian",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LibraryUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LibraryUserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LibraryUsername = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryUser_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_LibraryMemberId",
                table: "Reservation",
                column: "LibraryMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_LibraryMemberId",
                table: "Rating",
                column: "LibraryMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPayment_LibraryMemberId",
                table: "MembershipPayment",
                column: "LibraryMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryMember_LibraryUserId",
                table: "LibraryMember",
                column: "LibraryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Librarian_LibraryUserId",
                table: "Librarian",
                column: "LibraryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Librarian_LibraryUser_Id",
                table: "Librarian",
                column: "Id",
                principalTable: "LibraryUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Librarian_LibraryUser_LibraryUserId",
                table: "Librarian",
                column: "LibraryUserId",
                principalTable: "LibraryUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMember_LibraryUser_Id",
                table: "LibraryMember",
                column: "Id",
                principalTable: "LibraryUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryMember_LibraryUser_LibraryUserId",
                table: "LibraryMember",
                column: "LibraryUserId",
                principalTable: "LibraryUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipPayment_LibraryMember_LibraryMemberId",
                table: "MembershipPayment",
                column: "LibraryMemberId",
                principalTable: "LibraryMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_LibraryMember_LibraryMemberId",
                table: "Rating",
                column: "LibraryMemberId",
                principalTable: "LibraryMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_LibraryMember_LibraryMemberId",
                table: "Reservation",
                column: "LibraryMemberId",
                principalTable: "LibraryMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
