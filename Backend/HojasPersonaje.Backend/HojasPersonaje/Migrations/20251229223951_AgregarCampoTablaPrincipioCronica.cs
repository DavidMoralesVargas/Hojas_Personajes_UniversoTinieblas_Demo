using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoTablaPrincipioCronica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CronicaId",
                table: "Principios_Cronicas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CronicaId",
                table: "Principios_Cronicas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
