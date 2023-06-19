using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class attnewv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Departments_DeptID",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_DeptID",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DeptID",
                table: "Attendances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptID",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DeptID",
                table: "Attendances",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Departments_DeptID",
                table: "Attendances",
                column: "DeptID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
