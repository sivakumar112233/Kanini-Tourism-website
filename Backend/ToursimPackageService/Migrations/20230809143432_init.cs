using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursimPackageService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourPrice = table.Column<float>(type: "real", nullable: true),
                    TourLocationCountry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TourLocationState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TourLocationCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TourSpecialty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaxCount = table.Column<int>(type: "int", nullable: false),
                    TravelAgentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                });

            migrationBuilder.CreateTable(
                name: "Inclusion",
                columns: table => new
                {
                    InclusionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    Accommodation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Meals = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Transfer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalNights = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inclusion", x => x.InclusionId);
                    table.ForeignKey(
                        name: "FK_Inclusion_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalDaysDescriptions",
                columns: table => new
                {
                    TotalDaysDescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourSpotName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InclusionId = table.Column<int>(type: "int", nullable: false),
                    DayDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalDaysDescriptions", x => x.TotalDaysDescriptionId);
                    table.ForeignKey(
                        name: "FK_TotalDaysDescriptions_Inclusion_InclusionId",
                        column: x => x.InclusionId,
                        principalTable: "Inclusion",
                        principalColumn: "InclusionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inclusion_TourId",
                table: "Inclusion",
                column: "TourId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TotalDaysDescriptions_InclusionId",
                table: "TotalDaysDescriptions",
                column: "InclusionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalDaysDescriptions");

            migrationBuilder.DropTable(
                name: "Inclusion");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
