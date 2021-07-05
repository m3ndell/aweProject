using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aweProject.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Retouren_Ressources_RessourcesId",
                table: "Retouren");

            migrationBuilder.DropIndex(
                name: "IX_Retouren_RessourcesId",
                table: "Retouren");

            migrationBuilder.DropColumn(
                name: "RessourcesId",
                table: "Retouren");

            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "Retouren",
                newName: "RetourenTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Retouren",
                newName: "RetourenId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInTime",
                table: "Retouren",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Retouren",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "RessourceId",
                table: "Retouren",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SiteId",
                table: "Retouren",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInTime",
                table: "Retouren");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Retouren");

            migrationBuilder.DropColumn(
                name: "RessourceId",
                table: "Retouren");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "Retouren");

            migrationBuilder.RenameColumn(
                name: "RetourenTime",
                table: "Retouren",
                newName: "DeliveryTime");

            migrationBuilder.RenameColumn(
                name: "RetourenId",
                table: "Retouren",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "RessourcesId",
                table: "Retouren",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retouren_RessourcesId",
                table: "Retouren",
                column: "RessourcesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Retouren_Ressources_RessourcesId",
                table: "Retouren",
                column: "RessourcesId",
                principalTable: "Ressources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
