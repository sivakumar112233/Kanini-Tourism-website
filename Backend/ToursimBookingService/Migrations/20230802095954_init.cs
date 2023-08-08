using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursimBookingService.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravellerId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    NoOfPersons = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "BookingUsers",
                columns: table => new
                {
                    BookingUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    BookingUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingUserGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingUserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingUserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingUsers", x => x.BookingUserId);
                    table.ForeignKey(
                        name: "FK_BookingUsers_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingUsers_BookingId",
                table: "BookingUsers",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingUsers");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
