using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Entidades.ValidacionesDatos;
using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();     
builder.Services.AddSwaggerGen();           

//Inyecciones de los validadores
builder.Services.AddValidatorsFromAssemblyContaining<Atributo_Hoja_PersonajeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BackgroundValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BiografiaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Clan_BaneValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Conviccion_PiedrasValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CronicaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Disciplina_PersonajeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Disciplinas_VampirosRepositorio>();
builder.Services.AddValidatorsFromAssemblyContaining<DisciplinaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Especialidad_HabilidadValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Experiencia_HojaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FlawValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Habilidades_DisciplinaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<HabilidadesPersonajeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<HabilidadValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Hoja_PersonajeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Lista_AmigosValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MeritoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Nota_AdicionalValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Notas_PrincipalesValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PosesionesValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Principio_CronicaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Tipo_DepredadorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VampiroValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<WeaponValidator>();

//Inyección de la clase contexto
builder.Services.AddDbContext<ClaseContexto>(x => x.UseSqlServer("name=DockerConnection"));
builder.Services.AddTransient<SeedDb>();


//Inyecciones de implementaciones e interfaces
builder.Services.AddScoped(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));
builder.Services.AddScoped<IAtributos_Hoja_PersonajeRepositorio, Atributos_Hoja_PersonajeRepositorio>();
builder.Services.AddScoped<IBackgroundRepositorio, BackgroundRepositorio>();
builder.Services.AddScoped<IBiografiasRepositorio, BiografiasRepositorio>();
builder.Services.AddScoped<IClan_BanesRepositorio, Clan_BanesRepositorio>();
builder.Services.AddScoped<IConvicciones_PiedrasRepositorio, Convicciones_PiedrasRepositorio>();
builder.Services.AddScoped<ICronicasRepositorio, CronicasRepositorio>();
builder.Services.AddScoped<IDisciplinasRepositorio, DisciplinasRepositorio>();
builder.Services.AddScoped<IDisciplinas_VampirosRepositorio, Disciplinas_VampirosRepositorio>();
builder.Services.AddScoped<IDisciplinas_PersonajeRepositorio, Disciplinas_PersonajeRepositorio>();
builder.Services.AddScoped<IEspecialidades_HabilidadesRepositorio, Especialidades_HabilidadesRepositorio>();
builder.Services.AddScoped<IExperiencia_HojasRepositorio, Experiencia_HojasRepositorio>();
builder.Services.AddScoped<IFlawsRepositorio, FlawsRepositorio>();
builder.Services.AddScoped<IHabilidades_DisciplinasRepositorio, Habilidades_DisciplinasRepositorio>();
builder.Services.AddScoped<IHabilidadesPersonajesRepositorio, HabilidadesPersonajesRepositorio>();
builder.Services.AddScoped<IHabilidadesRepositorio, HabilidadesRepositorio>();
builder.Services.AddScoped<IHojas_PersonajeRepositorio, Hojas_PersonajeRepositorio>();
builder.Services.AddScoped<ILista_AmigosRepositorio, Lista_AmigosRepositorio>();
builder.Services.AddScoped<IMeritosRepositorio, MeritosRepositorio>();
builder.Services.AddScoped<INotas_AdicionalesRepositorio, Notas_AdicionalesRepositorio>();
builder.Services.AddScoped<INotas_PrincipalesRepositorio, Notas_PrincipalesRepositorio>();
builder.Services.AddScoped<IPosesionesRepositorio, PosesionesRepositorio>();
builder.Services.AddScoped<IPrincipios_CronicaRepositorio, Principios_CronicaRepositorio>();
builder.Services.AddScoped<ITipos_DepredadorRepositorio, Tipos_DepredadorRepositorio>();
builder.Services.AddScoped<IUsuariosRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IVampirosRepositorio, VampirosRepositorio>();
builder.Services.AddScoped<IWeaponsRepositorio, WeaponsRepositorio>();


builder.Services.AddIdentity<Usuario, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ClaseContexto>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
    ClockSkew = TimeSpan.Zero
});



var app = builder.Build();

SeedData(app);
void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());


if (app.Environment.IsDevelopment())     //Esto es un condicional para indicar que solo cuando esté en modo de DESARROLLO o depuración, este podrá mostrar el Swagger, si se quita se podrá mostrar el Swagger para producción
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
