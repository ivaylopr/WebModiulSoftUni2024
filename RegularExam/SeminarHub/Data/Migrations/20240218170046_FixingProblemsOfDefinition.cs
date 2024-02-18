using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class FixingProblemsOfDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seminars_AspNetUsers_OrganiserId",
                table: "Seminars");

            migrationBuilder.RenameColumn(
                name: "OrganiserId",
                table: "Seminars",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Seminars_OrganiserId",
                table: "Seminars",
                newName: "IX_Seminars_OrganizerId");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Seminars",
                type: "int",
                nullable: true,
                comment: "Duration of the seminar",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Duration of the seminar");

            migrationBuilder.AddForeignKey(
                name: "FK_Seminars_AspNetUsers_OrganizerId",
                table: "Seminars",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seminars_AspNetUsers_OrganizerId",
                table: "Seminars");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Seminars",
                newName: "OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_Seminars_OrganizerId",
                table: "Seminars",
                newName: "IX_Seminars_OrganiserId");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Seminars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Duration of the seminar",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Duration of the seminar");

            migrationBuilder.AddForeignKey(
                name: "FK_Seminars_AspNetUsers_OrganiserId",
                table: "Seminars",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
