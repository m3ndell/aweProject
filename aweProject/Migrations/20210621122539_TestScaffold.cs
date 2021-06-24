using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aweProject.Migrations
{
    public partial class TestScaffold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ressources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    BuyDate = table.Column<DateTime>(nullable: false),
                    NextMaintenance = table.Column<DateTime>(nullable: false),
                    Standort = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressources", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ressources");
        }
    }
}
