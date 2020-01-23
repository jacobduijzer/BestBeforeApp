using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestBeforeApp.EFConsole.Migrations
{
    public partial class AddPhotoByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Products");
        }
    }
}
