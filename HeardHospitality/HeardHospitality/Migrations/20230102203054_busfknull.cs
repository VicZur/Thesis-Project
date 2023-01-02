using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeardHospitality.Migrations
{
    public partial class busfknull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailsId",
                table: "Business");

            migrationBuilder.AlterColumn<string>(
                name: "LoginDetailsId",
                table: "Business",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailsId",
                table: "Business",
                column: "LoginDetailsId",
                principalTable: "LoginDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailsId",
                table: "Business");

            migrationBuilder.AlterColumn<string>(
                name: "LoginDetailsId",
                table: "Business",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailsId",
                table: "Business",
                column: "LoginDetailsId",
                principalTable: "LoginDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
