using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Disciplina = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_Depredador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Depredador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_Usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vampiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vampiros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades_Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Habilidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades_Disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habilidades_Disciplina_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cronicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Cronica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pais_Cronica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Cronica = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dungeon_MasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cronicas_Usuarios_Dungeon_MasterId",
                        column: x => x.Dungeon_MasterId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Listas_Amigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario1Id = table.Column<int>(type: "int", nullable: false),
                    Usuario2Id = table.Column<int>(type: "int", nullable: false),
                    Fecha_Amigos = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas_Amigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listas_Amigos_Usuarios_Usuario1Id",
                        column: x => x.Usuario1Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listas_Amigos_Usuarios_Usuario2Id",
                        column: x => x.Usuario2Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clan_Banes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bane = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compulsion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VampiroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clan_Banes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clan_Banes_Vampiros_VampiroId",
                        column: x => x.VampiroId,
                        principalTable: "Vampiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas_Vampiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VampiroId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas_Vampiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Vampiro_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Vampiro_Vampiros_VampiroId",
                        column: x => x.VampiroId,
                        principalTable: "Vampiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hojas_Personaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ambicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Generacion = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_DepredadorId = table.Column<int>(type: "int", nullable: false),
                    CronicaId = table.Column<int>(type: "int", nullable: false),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    Tipo_VampiroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hojas_Personaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hojas_Personaje_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hojas_Personaje_Tipos_Depredador_Tipo_DepredadorId",
                        column: x => x.Tipo_DepredadorId,
                        principalTable: "Tipos_Depredador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hojas_Personaje_Usuarios_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hojas_Personaje_Vampiros_Tipo_VampiroId",
                        column: x => x.Tipo_VampiroId,
                        principalTable: "Vampiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notas_Adicionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    CronicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas_Adicionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Adicionales_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Adicionales_Usuarios_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Principios_Cronicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Principios_Cronica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CronicaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principios_Cronicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Principios_Cronicas_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atributos_Hoja_Personaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fuerza = table.Column<int>(type: "int", nullable: false),
                    Destreza = table.Column<int>(type: "int", nullable: false),
                    Resistencia = table.Column<int>(type: "int", nullable: false),
                    Carisma = table.Column<int>(type: "int", nullable: false),
                    Manipulacion = table.Column<int>(type: "int", nullable: false),
                    Compostura = table.Column<int>(type: "int", nullable: false),
                    Inteligencia = table.Column<int>(type: "int", nullable: false),
                    Astucia = table.Column<int>(type: "int", nullable: false),
                    Resolucion = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atributos_Hoja_Personaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atributos_Hoja_Personaje_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Backgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Backgrounds_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Biografias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edad_Real = table.Column<int>(type: "int", nullable: false),
                    Edad_Aparente = table.Column<int>(type: "int", nullable: false),
                    Fecha_Nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Fecha_Muerte = table.Column<DateOnly>(type: "date", nullable: false),
                    Apariencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rasgos_Distintivos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Historia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biografias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biografias_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Convicciones_Piedras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Convicciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Piedras_Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convicciones_Piedras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Convicciones_Piedras_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas_Personaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas_Personaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Personaje_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Personaje_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experiencias_Hoja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Experiencia = table.Column<int>(type: "int", nullable: false),
                    Experiencia_Gastada = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencias_Hoja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiencias_Hoja_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flaws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flaws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flaws_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Atletismo = table.Column<int>(type: "int", nullable: false),
                    Pelea = table.Column<int>(type: "int", nullable: false),
                    Crafteo = table.Column<int>(type: "int", nullable: false),
                    Conduccion = table.Column<int>(type: "int", nullable: false),
                    Armas_Fuego = table.Column<int>(type: "int", nullable: false),
                    Latrocinio = table.Column<int>(type: "int", nullable: false),
                    Melee = table.Column<int>(type: "int", nullable: false),
                    Sigilo = table.Column<int>(type: "int", nullable: false),
                    Supervivencia = table.Column<int>(type: "int", nullable: false),
                    Animal_Ken = table.Column<int>(type: "int", nullable: false),
                    Etiqueta = table.Column<int>(type: "int", nullable: false),
                    Insight = table.Column<int>(type: "int", nullable: false),
                    Intimidacion = table.Column<int>(type: "int", nullable: false),
                    Liderazgo = table.Column<int>(type: "int", nullable: false),
                    Actuacion = table.Column<int>(type: "int", nullable: false),
                    Persuasion = table.Column<int>(type: "int", nullable: false),
                    Astucia = table.Column<int>(type: "int", nullable: false),
                    Subterfugio = table.Column<int>(type: "int", nullable: false),
                    Academicismo = table.Column<int>(type: "int", nullable: false),
                    Consciencia = table.Column<int>(type: "int", nullable: false),
                    Finanzas = table.Column<int>(type: "int", nullable: false),
                    Investigacion = table.Column<int>(type: "int", nullable: false),
                    Medicina = table.Column<int>(type: "int", nullable: false),
                    Ocultismo = table.Column<int>(type: "int", nullable: false),
                    Politica = table.Column<int>(type: "int", nullable: false),
                    Ciencia = table.Column<int>(type: "int", nullable: false),
                    Tecnologia = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habilidades_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadesPersonaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonajeId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadesPersonaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilidadesPersonaje_Habilidades_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Habilidades_Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabilidadesPersonaje_Hojas_Personaje_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meritos_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notas_Principales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas_Principales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Principales_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posesiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Posesion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posesiones_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    Hoja_PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Hojas_Personaje_Hoja_PersonajeId",
                        column: x => x.Hoja_PersonajeId,
                        principalTable: "Hojas_Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Especialidades_Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HabilidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades_Habilidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especialidades_Habilidades_Habilidades_HabilidadId",
                        column: x => x.HabilidadId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atributos_Hoja_Personaje_Hoja_PersonajeId",
                table: "Atributos_Hoja_Personaje",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Backgrounds_Hoja_PersonajeId",
                table: "Backgrounds",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Biografias_Hoja_PersonajeId",
                table: "Biografias",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clan_Banes_VampiroId",
                table: "Clan_Banes",
                column: "VampiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Convicciones_Piedras_Hoja_PersonajeId",
                table: "Convicciones_Piedras",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cronicas_Dungeon_MasterId",
                table: "Cronicas",
                column: "Dungeon_MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_Personaje_DisciplinaId",
                table: "Disciplinas_Personaje",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_Personaje_Hoja_PersonajeId",
                table: "Disciplinas_Personaje",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_Vampiro_DisciplinaId",
                table: "Disciplinas_Vampiro",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_Vampiro_VampiroId",
                table: "Disciplinas_Vampiro",
                column: "VampiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Especialidades_Habilidades_HabilidadId",
                table: "Especialidades_Habilidades",
                column: "HabilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_Hoja_Hoja_PersonajeId",
                table: "Experiencias_Hoja",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flaws_Hoja_PersonajeId",
                table: "Flaws",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_Hoja_PersonajeId",
                table: "Habilidades",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_Disciplina_DisciplinaId",
                table: "Habilidades_Disciplina",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesPersonaje_DisciplinaId",
                table: "HabilidadesPersonaje",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesPersonaje_PersonajeId",
                table: "HabilidadesPersonaje",
                column: "PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_CronicaId",
                table: "Hojas_Personaje",
                column: "CronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_JugadorId",
                table: "Hojas_Personaje",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_Tipo_DepredadorId",
                table: "Hojas_Personaje",
                column: "Tipo_DepredadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_Tipo_VampiroId",
                table: "Hojas_Personaje",
                column: "Tipo_VampiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario1Id",
                table: "Listas_Amigos",
                column: "Usuario1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario2Id",
                table: "Listas_Amigos",
                column: "Usuario2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Meritos_Hoja_PersonajeId",
                table: "Meritos",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Adicionales_CronicaId",
                table: "Notas_Adicionales",
                column: "CronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Adicionales_JugadorId",
                table: "Notas_Adicionales",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Principales_Hoja_PersonajeId",
                table: "Notas_Principales",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Posesiones_Hoja_PersonajeId",
                table: "Posesiones",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Principios_Cronicas_CronicaId",
                table: "Principios_Cronicas",
                column: "CronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_Hoja_PersonajeId",
                table: "Weapons",
                column: "Hoja_PersonajeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atributos_Hoja_Personaje");

            migrationBuilder.DropTable(
                name: "Backgrounds");

            migrationBuilder.DropTable(
                name: "Biografias");

            migrationBuilder.DropTable(
                name: "Clan_Banes");

            migrationBuilder.DropTable(
                name: "Convicciones_Piedras");

            migrationBuilder.DropTable(
                name: "Disciplinas_Personaje");

            migrationBuilder.DropTable(
                name: "Disciplinas_Vampiro");

            migrationBuilder.DropTable(
                name: "Especialidades_Habilidades");

            migrationBuilder.DropTable(
                name: "Experiencias_Hoja");

            migrationBuilder.DropTable(
                name: "Flaws");

            migrationBuilder.DropTable(
                name: "HabilidadesPersonaje");

            migrationBuilder.DropTable(
                name: "Listas_Amigos");

            migrationBuilder.DropTable(
                name: "Meritos");

            migrationBuilder.DropTable(
                name: "Notas_Adicionales");

            migrationBuilder.DropTable(
                name: "Notas_Principales");

            migrationBuilder.DropTable(
                name: "Posesiones");

            migrationBuilder.DropTable(
                name: "Principios_Cronicas");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Habilidades_Disciplina");

            migrationBuilder.DropTable(
                name: "Hojas_Personaje");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cronicas");

            migrationBuilder.DropTable(
                name: "Tipos_Depredador");

            migrationBuilder.DropTable(
                name: "Vampiros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
