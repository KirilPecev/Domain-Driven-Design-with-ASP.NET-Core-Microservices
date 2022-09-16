using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Infrastructure.Persistence.Migrations
{
    public partial class Statistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalCarAds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarAdViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarAdId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatisticId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAdViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarAdViews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarAdViews_CarAds_CarAdId",
                        column: x => x.CarAdId,
                        principalTable: "CarAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarAdViews_Statistics_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarAdViews_CarAdId",
                table: "CarAdViews",
                column: "CarAdId");

            migrationBuilder.CreateIndex(
                name: "IX_CarAdViews_StatisticId",
                table: "CarAdViews",
                column: "StatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_CarAdViews_UserId",
                table: "CarAdViews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarAdViews");

            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
