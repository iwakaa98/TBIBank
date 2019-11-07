using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBIApp.Data.Migrations
{
    public partial class AppUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_LoanApplicationStatuses_LoanApplicationStatusId",
                table: "LoanApplications");

            migrationBuilder.DropTable(
                name: "LoanApplicationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplications_LoanApplicationStatusId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "LoanApplicationStatusId",
                table: "LoanApplications");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LoanApplications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LoanApplications");

            migrationBuilder.AddColumn<string>(
                name: "LoanApplicationStatusId",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoanApplicationStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SetToTerminalStatus = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicationStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_LoanApplicationStatusId",
                table: "LoanApplications",
                column: "LoanApplicationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_LoanApplicationStatuses_LoanApplicationStatusId",
                table: "LoanApplications",
                column: "LoanApplicationStatusId",
                principalTable: "LoanApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
