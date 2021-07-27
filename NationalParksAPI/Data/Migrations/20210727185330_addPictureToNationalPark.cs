using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NationalParksAPI.Migrations
{
    public partial class addPictureToNationalPark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "picture",
                table: "NationalParks",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "picture",
                table: "NationalParks");
        }
    }
}
