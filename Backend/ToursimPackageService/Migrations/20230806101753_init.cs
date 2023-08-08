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
                name: "Inclusion",
                columns: table => new
                {
                    InclusionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    Accommodation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalNights = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inclusion", x => x.InclusionId);
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

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourPrice = table.Column<float>(type: "real", nullable: true),
                    TourLocationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourLocationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourLocationCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourSpecialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InclusionId = table.Column<int>(type: "int", nullable: false),
                    MaxCount = table.Column<int>(type: "int", nullable: false),
                    TravelAgentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Tours_Inclusion_InclusionId",
                        column: x => x.InclusionId,
                        principalTable: "Inclusion",
                        principalColumn: "InclusionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TotalDaysDescriptions_InclusionId",
                table: "TotalDaysDescriptions",
                column: "InclusionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_InclusionId",
                table: "Tours",
                column: "InclusionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalDaysDescriptions");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Inclusion");
        }
    }
}
