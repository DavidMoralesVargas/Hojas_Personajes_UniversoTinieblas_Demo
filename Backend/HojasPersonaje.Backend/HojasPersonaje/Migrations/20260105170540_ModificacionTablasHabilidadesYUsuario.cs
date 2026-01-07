using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablasHabilidadesYUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enardecimiento",
                table: "Habilidades_Disciplina",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tirada",
                table: "Habilidades_Disciplina",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enardecimiento",
                table: "Habilidades_Disciplina");

            migrationBuilder.DropColumn(
                name: "Tirada",
                table: "Habilidades_Disciplina");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "AspNetUsers");
        }
    }
}
