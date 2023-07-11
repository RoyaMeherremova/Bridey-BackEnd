using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrideyApp.Migrations
{
    public partial class DeleteColumnSubscribeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_AspNetUsers_AppUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_AppUserId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Subscribes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Subscribes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_AppUserId",
                table: "Subscribes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_AspNetUsers_AppUserId",
                table: "Subscribes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
