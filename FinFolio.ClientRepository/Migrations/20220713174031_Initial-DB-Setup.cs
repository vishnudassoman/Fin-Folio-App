using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinFolio.ClientRepository.Migrations
{
    public partial class InitialDBSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortFolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(225)", maxLength: 225, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortFolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ItemName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortFolioItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ItemName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    NoOfUnits = table.Column<int>(type: "int", nullable: false),
                    CostValue = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    PurchaseDateTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSIP = table.Column<bool>(type: "bit", nullable: false),
                    PortFolioItemType = table.Column<int>(type: "int", nullable: false),
                    PortFolioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortFolioItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortFolioItems_PortFolios_PortFolioId",
                        column: x => x.PortFolioId,
                        principalTable: "PortFolios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortFolioItems_PortFolioId",
                table: "PortFolioItems",
                column: "PortFolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortFolioItems");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "PortFolios");
        }
    }
}
