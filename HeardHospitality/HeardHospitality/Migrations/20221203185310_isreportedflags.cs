using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeardHospitality.Migrations
{
    public partial class isreportedflags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Rating",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "JobInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "JobInfo");
        }
    }
}
