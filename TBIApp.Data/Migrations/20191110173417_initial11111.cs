using Microsoft.EntityFrameworkCore.Migrations;

namespace TBIApp.Data.Migrations
{
    public partial class initial11111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LoanApplications",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "LoanApplications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "LoanApplications");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "LoanApplications",
                newName: "Name");
        }
    }
}
