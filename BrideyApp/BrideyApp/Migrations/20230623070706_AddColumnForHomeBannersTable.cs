using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrideyApp.Migrations
{
    public partial class AddColumnForHomeBannersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLarge",
                table: "HomeBanners");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "HomeBanners",
                newName: "SmallImage");

            migrationBuilder.AddColumn<string>(
                name: "LargeImage",
                table: "HomeBanners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeImage",
                table: "HomeBanners");

            migrationBuilder.RenameColumn(
                name: "SmallImage",
                table: "HomeBanners",
                newName: "Image");

            migrationBuilder.AddColumn<bool>(
                name: "IsLarge",
                table: "HomeBanners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
