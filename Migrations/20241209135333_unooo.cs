using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioVentas.Migrations
{
    /// <inheritdoc />
    public partial class unooo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "cliente");
        }
    }
}
