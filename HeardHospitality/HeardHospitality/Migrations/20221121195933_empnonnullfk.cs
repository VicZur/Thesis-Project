using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeardHospitality.Migrations
{
    public partial class empnonnullfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailsId",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "LoginDetailsId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailsId",
                table: "Employee",
                column: "LoginDetailsId",
                principalTable: "LoginDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailsId",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "LoginDetailsId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailsId",
                table: "Employee",
                column: "LoginDetailsId",
                principalTable: "LoginDetail",
                principalColumn: "Id");
        }
    }
}
