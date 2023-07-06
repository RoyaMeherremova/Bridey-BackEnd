using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrideyApp.Migrations
{
    public partial class DeleteColumnWishlistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "WishlistProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "WishlistProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
