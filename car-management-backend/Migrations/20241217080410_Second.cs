using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_management_backend.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garages_Cars_CarId",
                table: "Garages");

            migrationBuilder.DropIndex(
                name: "IX_Garages_CarId",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Garages");

            migrationBuilder.CreateTable(
                name: "CarGarage",
                columns: table => new
                {
                    CarGarageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    GarageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGarage", x => x.CarGarageId);
                    table.ForeignKey(
                        name: "FK_CarGarage_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId");
                    table.ForeignKey(
                        name: "FK_CarGarage_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "GarageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarGarage_CarId",
                table: "CarGarage",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarGarage_GarageId",
                table: "CarGarage",
                column: "GarageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarGarage");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Garages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Garages_CarId",
                table: "Garages",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Garages_Cars_CarId",
                table: "Garages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId");
        }
    }
}
