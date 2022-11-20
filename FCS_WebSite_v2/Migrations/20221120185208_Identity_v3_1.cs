using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCS_WebSite_v2.Migrations
{
    public partial class Identity_v3_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FormTime",
                schema: "Identity",
                table: "FormQuestionAnswer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormTime",
                schema: "Identity",
                table: "FormQuestionAnswer");
        }
    }
}
