using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoTablaCronica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Cronicas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Cronicas");
        }
    }
}
