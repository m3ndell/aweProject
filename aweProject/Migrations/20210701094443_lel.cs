using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aweProject.Migrations
{
    public partial class lel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Ressources_RessourceId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_RessourceId",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "RessourceId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SiteID",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteID",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "RessourceId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Order_RessourceId",
                table: "Order",
                column: "RessourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Ressources_RessourceId",
                table: "Order",
                column: "RessourceId",
                principalTable: "Ressources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
