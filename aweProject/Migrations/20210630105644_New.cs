using Microsoft.EntityFrameworkCore.Migrations;

namespace aweProject.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInStorage",
                table: "Ressources",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrderLog",
                table: "Ressources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInStorage",
                table: "Ressources");

            migrationBuilder.DropColumn(
                name: "OrderLog",
                table: "Ressources");
        }
    }
}
