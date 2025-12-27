using HojasPersonaje.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace HojasPersonaje.Data
{
    public class ClaseContexto : DbContext
    {
        public ClaseContexto(DbContextOptions<ClaseContexto> options) : base(options) {}


        public DbSet<Atributo_Hoja_Personaje> Atributos_Hoja_Personaje { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<Biografia> Biografias { get; set; }
        public DbSet<Clan_Bane> Clan_Banes { get; set; }
        public DbSet<Conviccion_Piedras> Convicciones_Piedras { get; set; }
        public DbSet<Cronica> Cronicas { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Disciplina_Personaje> Disciplinas_Personaje { get; set; }
        public DbSet<Disciplina_Vampiro> Disciplinas_Vampiro { get; set; }
        public DbSet<Especialidad_Habilidad> Especialidades_Habilidades { get; set; }
        public DbSet<Experiencia_Hoja> Experiencias_Hoja { get; set; }
        public DbSet<Flaw> Flaws { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }
        public DbSet<Habilidades_Disciplina> Habilidades_Disciplina { get; set; }
        public DbSet<HabilidadesPersonaje> HabilidadesPersonaje { get; set; }
        public DbSet<Hoja_Personaje> Hojas_Personaje { get; set; }
        public DbSet<Lista_Amigos> Listas_Amigos { get; set; }
        public DbSet<Merito> Meritos { get; set; }
        public DbSet<Nota_Adicional> Notas_Adicionales { get; set; }
        public DbSet<Notas_Principales> Notas_Principales { get; set; }
        public DbSet<Posesiones> Posesiones { get; set; }
        public DbSet<Principio_Cronica> Principios_Cronicas { get; set; }
        public DbSet<Tipo_Depredador> Tipos_Depredador { get; set; }
        public DbSet<Vampiro> Vampiros { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DisableCascadingDelete(modelBuilder);

        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
