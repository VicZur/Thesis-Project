using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeardHospitality.Migrations
{
    public partial class modelbuildertotable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Business_AspNetUsers_LoginDetailsId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_LoginDetailsId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "LoginDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginDetail",
                table: "LoginDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_LoginDetail_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "LoginDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_LoginDetail_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "LoginDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_LoginDetail_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "LoginDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_LoginDetail_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "LoginDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailsId",
                table: "Business",
                column: "LoginDetailsId",
                principalTable: "LoginDetail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailsId",
                table: "Employee",
                column: "LoginDetailsId",
                principalTable: "LoginDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_LoginDetail_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_LoginDetail_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_LoginDetail_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_LoginDetail_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Business_LoginDetail_LoginDetailsId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_LoginDetail_LoginDetailsId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginDetail",
                table: "LoginDetail");

            migrationBuilder.RenameTable(
                name: "LoginDetail",
                newName: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
