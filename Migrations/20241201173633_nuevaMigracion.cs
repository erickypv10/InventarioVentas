using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioVentas.Migrations
{
    /// <inheritdoc />
    public partial class nuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "venta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_venta_ClienteId",
                table: "venta",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_venta_cliente_ClienteId",
                table: "venta",
                column: "ClienteId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_venta_cliente_ClienteId",
                table: "venta");

            migrationBuilder.DropIndex(
                name: "IX_venta_ClienteId",
                table: "venta");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "venta");
        }
    }
}
