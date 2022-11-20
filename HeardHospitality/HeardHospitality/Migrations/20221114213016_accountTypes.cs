using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeardHospitality.Migrations
{
    public partial class accountTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "isBusinessAccount",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isEmployeeAccount",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBusinessAccount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isEmployeeAccount",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
