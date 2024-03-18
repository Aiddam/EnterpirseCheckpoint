using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseCheckpoint.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MinorUserchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "HashKey",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "HashKey");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
