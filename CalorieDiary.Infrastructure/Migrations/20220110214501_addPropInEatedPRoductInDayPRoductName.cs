using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieDiary.Infrastructure.Migrations
{
    public partial class addPropInEatedPRoductInDayPRoductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "EatedProductInDays",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "EatedProductInDays");
        }
    }
}
