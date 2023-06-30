using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrideyApp.Migrations
{
    public partial class DeleteColumnInProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVideos_ProductId",
                table: "ProductVideos");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVideos_ProductId",
                table: "ProductVideos",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVideos_ProductId",
                table: "ProductVideos");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVideos_ProductId",
                table: "ProductVideos",
                column: "ProductId");
        }
    }
}
