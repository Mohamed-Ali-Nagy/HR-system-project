using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_ApplicationUserGroupVM_ApplicationUserGroupVMId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ApplicationUserGroupVMId",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGroupVM",
                table: "ApplicationUserGroupVM");

            migrationBuilder.DropColumn(
                name: "ApplicationUserGroupVMId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUserGroupVM");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserGroupVMUserName",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ApplicationUserGroupVM",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGroupVM",
                table: "ApplicationUserGroupVM",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ApplicationUserGroupVMUserName",
                table: "AspNetRoles",
                column: "ApplicationUserGroupVMUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_ApplicationUserGroupVM_ApplicationUserGroupVMUserName",
                table: "AspNetRoles",
                column: "ApplicationUserGroupVMUserName",
                principalTable: "ApplicationUserGroupVM",
                principalColumn: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_ApplicationUserGroupVM_ApplicationUserGroupVMUserName",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ApplicationUserGroupVMUserName",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGroupVM",
                table: "ApplicationUserGroupVM");

            migrationBuilder.DropColumn(
                name: "ApplicationUserGroupVMUserName",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserGroupVMId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ApplicationUserGroupVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApplicationUserGroupVM",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGroupVM",
                table: "ApplicationUserGroupVM",
                column: "Id");

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
    }
}
