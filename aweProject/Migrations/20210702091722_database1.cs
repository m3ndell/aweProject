using Microsoft.EntityFrameworkCore.Migrations;

namespace aweProject.Migrations
{
    public partial class database1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiteID",
                table: "Order",
                newName: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "Order",
                newName: "SiteID");
        }
    }
}
