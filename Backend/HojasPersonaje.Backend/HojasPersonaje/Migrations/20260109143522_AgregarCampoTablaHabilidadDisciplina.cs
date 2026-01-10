using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoTablaHabilidadDisciplina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TiradaEnfrentada",
                table: "Habilidades_Disciplina",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiradaEnfrentada",
                table: "Habilidades_Disciplina");
        }
    }
}
