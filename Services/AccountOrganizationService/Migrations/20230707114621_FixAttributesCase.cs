using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountOrganizationService2.Migrations
{
    /// <inheritdoc />
    public partial class FixAttributesCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUser_Users_Managersid",
                table: "DepartmentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_Supervisorid",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "zip",
                table: "Users",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Users",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Users",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Supervisorid",
                table: "Users",
                newName: "SupervisorId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Supervisorid",
                table: "Users",
                newName: "IX_Users_SupervisorId");

            migrationBuilder.RenameColumn(
                name: "Managersid",
                table: "DepartmentUser",
                newName: "ManagersId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentUser_Managersid",
                table: "DepartmentUser",
                newName: "IX_DepartmentUser_ManagersId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUser_Users_ManagersId",
                table: "DepartmentUser",
                column: "ManagersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_SupervisorId",
                table: "Users",
                column: "SupervisorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUser_Users_ManagersId",
                table: "DepartmentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_SupervisorId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Users",
                newName: "zip");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Users",
                newName: "Supervisorid");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SupervisorId",
                table: "Users",
                newName: "IX_Users_Supervisorid");

            migrationBuilder.RenameColumn(
                name: "ManagersId",
                table: "DepartmentUser",
                newName: "Managersid");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentUser_ManagersId",
                table: "DepartmentUser",
                newName: "IX_DepartmentUser_Managersid");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUser_Users_Managersid",
                table: "DepartmentUser",
                column: "Managersid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_Supervisorid",
                table: "Users",
                column: "Supervisorid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
