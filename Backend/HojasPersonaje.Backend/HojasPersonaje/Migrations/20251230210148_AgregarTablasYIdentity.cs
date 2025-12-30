using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablasYIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre_Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_Usuario = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    Dungeon_MasterId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Dungeon_MasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cronicas_AspNetUsers_Dungeon_MasterId1",
                        column: x => x.Dungeon_MasterId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Listas_Amigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario1Id1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Usuario1Id = table.Column<int>(type: "int", nullable: false),
                    Usuario2Id1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Usuario2Id = table.Column<int>(type: "int", nullable: false),
                    Fecha_Amigos = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas_Amigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listas_Amigos_AspNetUsers_Usuario1Id1",
                        column: x => x.Usuario1Id1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listas_Amigos_AspNetUsers_Usuario2Id1",
                        column: x => x.Usuario2Id1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    JugadorId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    Tipo_VampiroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hojas_Personaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hojas_Personaje_AspNetUsers_JugadorId1",
                        column: x => x.JugadorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    JugadorId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    CronicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas_Adicionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Adicionales_AspNetUsers_JugadorId1",
                        column: x => x.JugadorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Adicionales_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
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
                    CronicaId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Cronicas_Dungeon_MasterId1",
                table: "Cronicas",
                column: "Dungeon_MasterId1");

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
                name: "IX_Hojas_Personaje_JugadorId1",
                table: "Hojas_Personaje",
                column: "JugadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_Tipo_DepredadorId",
                table: "Hojas_Personaje",
                column: "Tipo_DepredadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hojas_Personaje_Tipo_VampiroId",
                table: "Hojas_Personaje",
                column: "Tipo_VampiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario1Id1",
                table: "Listas_Amigos",
                column: "Usuario1Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_Amigos_Usuario2Id1",
                table: "Listas_Amigos",
                column: "Usuario2Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Meritos_Hoja_PersonajeId",
                table: "Meritos",
                column: "Hoja_PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Adicionales_CronicaId",
                table: "Notas_Adicionales",
                column: "CronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_Adicionales_JugadorId1",
                table: "Notas_Adicionales",
                column: "JugadorId1");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "AspNetRoles");

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
                name: "AspNetUsers");
        }
    }
}
