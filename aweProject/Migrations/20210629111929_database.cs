using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aweProject.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SiteId",
                table: "Ressources",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    RessourceId = table.Column<Guid>(nullable: true),
                    CheckOutTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Ressources_RessourceId",
                        column: x => x.RessourceId,
                        principalTable: "Ressources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Retouren",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    RessourcesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retouren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retouren_Ressources_RessourcesId",
                        column: x => x.RessourcesId,
                        principalTable: "Ressources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    manager = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteManagement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_RessourceId",
                table: "Order",
                column: "RessourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Retouren_RessourcesId",
                table: "Retouren",
                column: "RessourcesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Retouren");

            migrationBuilder.DropTable(
                name: "SiteManagement");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "Ressources");
        }
    }
}
