using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL_KTPM.Data.Migrations
{
    public partial class editAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "AppUser");
        }
    }
}
