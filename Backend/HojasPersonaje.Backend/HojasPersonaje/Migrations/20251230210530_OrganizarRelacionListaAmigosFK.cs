using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class OrganizarRelacionListaAmigosFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario1Id1",
                table: "Listas_Amigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario2Id1",
                table: "Listas_Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Listas_Amigos_Usuario1Id1",
                table: "Listas_Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Listas_Amigos_Usuario2Id1",
                table: "Listas_Amigos");

            migrationBuilder.DropColumn(
                name: "Usuario1Id1",
                table: "Listas_Amigos");

            migrationBuilder.DropColumn(
                name: "Usuario2Id1",
                table: "Listas_Amigos");

            migrationBuilder.AlterColumn<string>(
                name: "Usuario2Id",
                table: "Listas_Amigos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Usuario1Id",
                table: "Listas_Amigos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario1Id",
                table: "Listas_Amigos",
                column: "Usuario1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario2Id",
                table: "Listas_Amigos",
                column: "Usuario2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario1Id",
                table: "Listas_Amigos",
                column: "Usuario1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario2Id",
                table: "Listas_Amigos",
                column: "Usuario2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario1Id",
                table: "Listas_Amigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario2Id",
                table: "Listas_Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Listas_Amigos_Usuario1Id",
                table: "Listas_Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Listas_Amigos_Usuario2Id",
                table: "Listas_Amigos");

            migrationBuilder.AlterColumn<int>(
                name: "Usuario2Id",
                table: "Listas_Amigos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Usuario1Id",
                table: "Listas_Amigos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usuario1Id1",
                table: "Listas_Amigos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usuario2Id1",
                table: "Listas_Amigos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario1Id1",
                table: "Listas_Amigos",
                column: "Usuario1Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario2Id1",
                table: "Listas_Amigos",
                column: "Usuario2Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario1Id1",
                table: "Listas_Amigos",
                column: "Usuario1Id1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_Amigos_AspNetUsers_Usuario2Id1",
                table: "Listas_Amigos",
                column: "Usuario2Id1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
