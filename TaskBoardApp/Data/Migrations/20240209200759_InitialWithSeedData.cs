using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class InitialWithSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Name of the board")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                },
                comment: "Board of the tasks");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Title of the task"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Description of the task"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date when the task is created"),
                    BoardId = table.Column<int>(type: "int", nullable: true, comment: "Identifier of the board who is on the task"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The user who owe the task")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Board Tasks");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3dd878bd-2c38-447d-a5d9-7ac6063b481c", 0, "6322a78f-b801-48b3-a386-cfbac1c2baf4", null, false, false, null, null, "SOFTUNI@TEST.BG", "AQAAAAEAACcQAAAAELGkgP60JtnxfwNSoqYFDurUdzWZTgiTO1EUDx9sCDCjvFfPrEeQMXrNixWURw1r3w==", null, false, "e88b0e3c-cdf3-48a5-b12f-0845898af514", false, "softuni@test.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 24, 22, 7, 58, 822, DateTimeKind.Local).AddTicks(9616), "Improve better styling for all public pages", "3dd878bd-2c38-447d-a5d9-7ac6063b481c", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 9, 22, 7, 58, 822, DateTimeKind.Local).AddTicks(9664), "Create android client app for the Task Restful Api", "3dd878bd-2c38-447d-a5d9-7ac6063b481c", "Android client App" },
                    { 3, 1, new DateTime(2023, 2, 9, 22, 7, 58, 822, DateTimeKind.Local).AddTicks(9671), "Create Windows Forms desktop app for the TaskBoard", "3dd878bd-2c38-447d-a5d9-7ac6063b481c", "Desktop client App" },
                    { 4, 1, new DateTime(2023, 2, 9, 22, 7, 58, 822, DateTimeKind.Local).AddTicks(9676), "Implement [Create Task] page for adding new tasks", "3dd878bd-2c38-447d-a5d9-7ac6063b481c", "Create Task" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dd878bd-2c38-447d-a5d9-7ac6063b481c");
        }
    }
}
