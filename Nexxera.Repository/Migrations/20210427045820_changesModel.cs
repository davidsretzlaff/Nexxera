using Microsoft.EntityFrameworkCore.Migrations;

namespace Nexxera.Repository.Migrations
{
    public partial class changesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CreditCardHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CreditCardHistories");
        }
    }
}
