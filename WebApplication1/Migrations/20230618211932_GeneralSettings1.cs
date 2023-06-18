using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class GeneralSettings1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddHourRate = table.Column<double>(type: "float", nullable: false),
                    DeducateHourRate = table.Column<double>(type: "float", nullable: false),
                    WeekRest1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekRest2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralSettings");
        }
    }
}
