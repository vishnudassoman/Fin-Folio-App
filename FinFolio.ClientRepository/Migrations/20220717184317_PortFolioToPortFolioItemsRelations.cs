using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinFolio.PortFolioRepository.Migrations
{
    public partial class PortFolioToPortFolioItemsRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortFolioItems_PortFolios_PortFolioId",
                table: "PortFolioItems");

            migrationBuilder.AlterColumn<int>(
                name: "PortFolioId",
                table: "PortFolioItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PortFolio_PortFolioItem",
                table: "PortFolioItems",
                column: "PortFolioId",
                principalTable: "PortFolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortFolio_PortFolioItem",
                table: "PortFolioItems");

            migrationBuilder.AlterColumn<int>(
                name: "PortFolioId",
                table: "PortFolioItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PortFolioItems_PortFolios_PortFolioId",
                table: "PortFolioItems",
                column: "PortFolioId",
                principalTable: "PortFolios",
                principalColumn: "Id");
        }
    }
}
