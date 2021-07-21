using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class WourHoursEntityExtendedAuditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "WorkHours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "WorkHours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WorkHours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "WorkHours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "WorkHours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "WorkHours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "WorkHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_EmployerId",
                table: "WorkHours",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_UserId",
                table: "WorkHours",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHours_Employers_EmployerId",
                table: "WorkHours",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHours_Users_UserId",
                table: "WorkHours",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkHours_Employers_EmployerId",
                table: "WorkHours");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkHours_Users_UserId",
                table: "WorkHours");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_WorkHours_EmployerId",
                table: "WorkHours");

            migrationBuilder.DropIndex(
                name: "IX_WorkHours_UserId",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkHours");
        }
    }
}
