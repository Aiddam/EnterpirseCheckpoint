using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseCheckpoint.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addstartandenddayofweek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekEnd",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekStart",
                table: "Employees",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeekEnd",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DayOfWeekStart",
                table: "Employees");
        }
    }
}
