using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinFolio.ClientRepository.Migrations
{
    public partial class SchemeMasterTableWithRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "PortFolioItems");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "PortFolioItems");

            migrationBuilder.DropColumn(
                name: "PurchaseDateTimeUTC",
                table: "PortFolioItems");

            migrationBuilder.AddColumn<int>(
                name: "SchemeId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "PortFolioItems",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SchemeId",
                table: "PortFolioItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    NAVName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_SchemeId",
                table: "Wishlist",
                column: "SchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_PortFolioItems_SchemeId",
                table: "PortFolioItems",
                column: "SchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scheme_PortFolioItem",
                table: "PortFolioItems",
                column: "SchemeId",
                principalTable: "Schemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scheme_Wishlist",
                table: "Wishlist",
                column: "SchemeId",
                principalTable: "Schemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scheme_PortFolioItem",
                table: "PortFolioItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheme_Wishlist",
                table: "Wishlist");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_SchemeId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_PortFolioItems_SchemeId",
                table: "PortFolioItems");

            migrationBuilder.DropColumn(
                name: "SchemeId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "PortFolioItems");

            migrationBuilder.DropColumn(
                name: "SchemeId",
                table: "PortFolioItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "Wishlist",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Wishlist",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "PortFolioItems",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "PortFolioItems",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDateTimeUTC",
                table: "PortFolioItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
