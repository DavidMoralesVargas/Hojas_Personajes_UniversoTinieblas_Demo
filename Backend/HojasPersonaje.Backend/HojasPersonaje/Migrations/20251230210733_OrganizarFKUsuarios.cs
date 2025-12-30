using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class OrganizarFKUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronicas_AspNetUsers_Dungeon_MasterId1",
                table: "Cronicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Hojas_Personaje_AspNetUsers_JugadorId1",
                table: "Hojas_Personaje");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Adicionales_AspNetUsers_JugadorId1",
                table: "Notas_Adicionales");

            migrationBuilder.DropIndex(
                name: "IX_Notas_Adicionales_JugadorId1",
                table: "Notas_Adicionales");

            migrationBuilder.DropIndex(
                name: "IX_Hojas_Personaje_JugadorId1",
                table: "Hojas_Personaje");

            migrationBuilder.DropIndex(
                name: "IX_Cronicas_Dungeon_MasterId1",
                table: "Cronicas");

            migrationBuilder.DropColumn(
                name: "JugadorId1",
                table: "Notas_Adicionales");

            migrationBuilder.DropColumn(
                name: "JugadorId1",
                table: "Hojas_Personaje");

            migrationBuilder.DropColumn(
                name: "Dungeon_MasterId1",
                table: "Cronicas");

            migrationBuilder.AlterColumn<string>(
                name: "JugadorId",
                table: "Notas_Adicionales",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "JugadorId",
                table: "Hojas_Personaje",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Dungeon_MasterId",
                table: "Cronicas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Adicionales_JugadorId",
                table: "Notas_Adicionales",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_JugadorId",
                table: "Hojas_Personaje",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cronicas_Dungeon_MasterId",
                table: "Cronicas",
                column: "Dungeon_MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cronicas_AspNetUsers_Dungeon_MasterId",
                table: "Cronicas",
                column: "Dungeon_MasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hojas_Personaje_AspNetUsers_JugadorId",
                table: "Hojas_Personaje",
                column: "JugadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Adicionales_AspNetUsers_JugadorId",
                table: "Notas_Adicionales",
                column: "JugadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronicas_AspNetUsers_Dungeon_MasterId",
                table: "Cronicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Hojas_Personaje_AspNetUsers_JugadorId",
                table: "Hojas_Personaje");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Adicionales_AspNetUsers_JugadorId",
                table: "Notas_Adicionales");

            migrationBuilder.DropIndex(
                name: "IX_Notas_Adicionales_JugadorId",
                table: "Notas_Adicionales");

            migrationBuilder.DropIndex(
                name: "IX_Hojas_Personaje_JugadorId",
                table: "Hojas_Personaje");

            migrationBuilder.DropIndex(
                name: "IX_Cronicas_Dungeon_MasterId",
                table: "Cronicas");

            migrationBuilder.AlterColumn<int>(
                name: "JugadorId",
                table: "Notas_Adicionales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JugadorId1",
                table: "Notas_Adicionales",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JugadorId",
                table: "Hojas_Personaje",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JugadorId1",
                table: "Hojas_Personaje",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dungeon_MasterId",
                table: "Cronicas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dungeon_MasterId1",
                table: "Cronicas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Adicionales_JugadorId1",
                table: "Notas_Adicionales",
                column: "JugadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_JugadorId1",
                table: "Hojas_Personaje",
                column: "JugadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cronicas_Dungeon_MasterId1",
                table: "Cronicas",
                column: "Dungeon_MasterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cronicas_AspNetUsers_Dungeon_MasterId1",
                table: "Cronicas",
                column: "Dungeon_MasterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hojas_Personaje_AspNetUsers_JugadorId1",
                table: "Hojas_Personaje",
                column: "JugadorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Adicionales_AspNetUsers_JugadorId1",
                table: "Notas_Adicionales",
                column: "JugadorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
