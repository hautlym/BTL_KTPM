using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL_KTPM.Data.Migrations
{
    public partial class editEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Producers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SĐT",
                table: "Producers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "SĐT",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "News");
        }
    }
}
