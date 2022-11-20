using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeardHospitality.Migrations
{
    public partial class customFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailID",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "LoginDetail");

            migrationBuilder.DropIndex(
                name: "IX_Employee_LoginDetailID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Business_LoginDetailId",
                table: "Business");

            migrationBuilder.AddColumn<string>(
                name: "LoginDetailsId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoginDetailsId",
                table: "Business",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LoginDetailId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LoginDetailsId",
                table: "Employee",
                column: "LoginDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_LoginDetailsId",
                table: "Business",
                column: "LoginDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_AspNetUsers_LoginDetailsId",
                table: "Business",
                column: "LoginDetailsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_LoginDetailsId",
                table: "Employee",
                column: "LoginDetailsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_AspNetUsers_LoginDetailsId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_LoginDetailsId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_LoginDetailsId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Business_LoginDetailsId",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "LoginDetailsId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LoginDetailsId",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LoginDetailId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "LoginDetail",
                columns: table => new
                {
                    LoginDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDetail", x => x.LoginDetailId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LoginDetailID",
                table: "Employee",
                column: "LoginDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Business_LoginDetailId",
                table: "Business",
                column: "LoginDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailId",
                table: "Business",
                column: "LoginDetailId",
                principalTable: "LoginDetail",
                principalColumn: "LoginDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailID",
                table: "Employee",
                column: "LoginDetailID",
                principalTable: "LoginDetail",
                principalColumn: "LoginDetailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
