using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieDiary.Infrastructure.Migrations
{
    public partial class AddPropInCaloricDemandAgeAndCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "CaloricDemands");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "CaloricDemands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "CaloricDemands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaloricDemands",
                table: "CaloricDemands",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CaloricDemands",
                table: "CaloricDemands");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "CaloricDemands");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CaloricDemands");

            migrationBuilder.RenameTable(
                name: "CaloricDemands",
                newName: "MyProperty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "Id");
        }
    }
}
