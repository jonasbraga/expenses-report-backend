using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountOrganizationService2.Migrations
{
    /// <inheritdoc />
    public partial class AddingAdminUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "City", "Country", "Email", "FirstName", "LastName", "Password", "Role", "State", "SupervisorId", "Zip" },
                values: new object[] { "1077b9dd-ee20-48a6-94a0-06c74fc2e8fe", "123 Admin St", "Admin City", "Admin Country", "admin@expense.report", "Admin", "Braga", "$2a$11$9ryqBDGTn.r4/u2TCt3hV.2.DDZCBr1rqbgHhmoqTOau0NxKiuii.", 3, "Admin State", "1077b9dd-ee20-48a6-94a0-06c74fc2e8fe", "12345" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1077b9dd-ee20-48a6-94a0-06c74fc2e8fe");
        }
    }
}
