using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserGroupVMId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroupVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroupVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ApplicationUserGroupVMId",
                table: "AspNetRoles",
                column: "ApplicationUserGroupVMId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_ApplicationUserGroupVM_ApplicationUserGroupVMId",
                table: "AspNetRoles",
                column: "ApplicationUserGroupVMId",
                principalTable: "ApplicationUserGroupVM",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_ApplicationUserGroupVM_ApplicationUserGroupVMId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUserGroupVM");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ApplicationUserGroupVMId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserGroupVMId",
                table: "AspNetRoles");
        }
    }
}
