using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountOrganizationService2.Migrations
{
    /// <inheritdoc />
    public partial class fixRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_SupervisorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Users",
                newName: "Supervisorid");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SupervisorId",
                table: "Users",
                newName: "IX_Users_Supervisorid");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_Supervisorid",
                table: "Users",
                column: "Supervisorid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_Supervisorid",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Supervisorid",
                table: "Users",
                newName: "SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Supervisorid",
                table: "Users",
                newName: "IX_Users_SupervisorId");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_SupervisorId",
                table: "Users",
                column: "SupervisorId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
