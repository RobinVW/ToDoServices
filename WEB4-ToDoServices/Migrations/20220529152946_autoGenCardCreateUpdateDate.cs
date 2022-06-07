using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB4_ToDoServices.Migrations
{
    public partial class autoGenCardCreateUpdateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateChanged",
                table: "Cards");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Cards",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Cards");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateChanged",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
