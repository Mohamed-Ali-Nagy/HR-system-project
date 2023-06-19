using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Departments_departmentID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_employeeID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_departmentID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_departmentID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_departmentID",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_employeeID",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "departmentID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "departmentID",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "employeeID",
                table: "Attendances");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptID",
                table: "Employees",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DeptID",
                table: "Attendances",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmpID",
                table: "Attendances",
                column: "EmpID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Departments_DeptID",
                table: "Attendances",
                column: "DeptID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmpID",
                table: "Attendances",
                column: "EmpID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DeptID",
                table: "Employees",
                column: "DeptID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Departments_DeptID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmpID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DeptID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DeptID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_DeptID",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EmpID",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "departmentID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "departmentID",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "employeeID",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_departmentID",
                table: "Employees",
                column: "departmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_departmentID",
                table: "Attendances",
                column: "departmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_employeeID",
                table: "Attendances",
                column: "employeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Departments_departmentID",
                table: "Attendances",
                column: "departmentID",
                principalTable: "Departments",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_employeeID",
                table: "Attendances",
                column: "employeeID",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_departmentID",
                table: "Employees",
                column: "departmentID",
                principalTable: "Departments",
                principalColumn: "ID");
        }
    }
}
