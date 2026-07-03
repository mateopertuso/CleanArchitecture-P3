using CasosUso.InterfacesCU;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.Servicios;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//////////////////////////// INICIO JWT ////////////////////////////////////

var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

builder.Services.AddAuthentication(aut =>
{
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(aut =>
{
    aut.RequireHttpsMetadata = false;
    aut.SaveToken = true;
    aut.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = ClaimTypes.Role,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

/////////////////////////////// FIN JWT /////////////////////////////////////
///
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1"
    });
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme, // "Bearer"
        BearerFormat = "JWT",
        Description = "Ingrese el token JWT. No es necesario escribir la palabra Bearer."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

});



builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosBD>();
builder.Services.AddScoped<IRepositorioRoles, RepositorioRolesBD>();
builder.Services.AddScoped<IRepositorioEquipos, RepositorioEquiposBD>();
builder.Services.AddScoped<IRepositorioPrestamos, RepositorioPrestamosBD>();
builder.Services.AddScoped<IRepositorioAuditoriaPrestamos, RepositorioAuditoriaPrestamosBD>();
builder.Services.AddScoped<IRepositorioObjetosCelestes, RepositorioObjetosCelestesBD>();
builder.Services.AddScoped<IRepositorioObservaciones, RepositorioObservacionesBD>();
builder.Services.AddScoped<ILogin, CULogin>();
builder.Services.AddScoped<IAltaUsuario, CUAltaUsuario>();
builder.Services.AddScoped<IAltaTelescopio, CUAltaTelescopio>();
builder.Services.AddScoped<IAltaMontura, CUAltaMontura>();
builder.Services.AddScoped<IAltaCamara, CUAltaCamara>();
builder.Services.AddScoped<IAltaOcular, CUAltaOcular>();
builder.Services.AddScoped<IListadoEquipos, CUListadoEquipos>();
builder.Services.AddScoped<IBajaEquipo, CUBajaEquipo>();
builder.Services.AddScoped<IModificarTelescopio, CUModificarTelescopio>();
builder.Services.AddScoped<IModificarMontura, CUModificarMontura>();
builder.Services.AddScoped<IModificarCamara, CUModificarCamara>();
builder.Services.AddScoped<IModificarOcular, CUModificarOcular>();
builder.Services.AddScoped<IBuscarEquipoPorId, CUBuscarEquipoPorId>();
builder.Services.AddScoped<IBuscarTelescopioPorId,  CUBuscarTelescopioPorId>();
builder.Services.AddScoped<IBuscarMonturaPorId, CUBuscarMonturaPorId>();
builder.Services.AddScoped<IBuscarCamaraPorId, CUBuscarCamaraPorId>();
builder.Services.AddScoped<IBuscarOcularPorId, CUBuscarOcularPorId>();
builder.Services.AddScoped<ICrearPrestamo, CUCrearPrestamo>();
builder.Services.AddScoped<IVerificarDisponibilidadEquipo, CUVerificarDisponibilidadEquipo>();
builder.Services.AddScoped<IListadoUsuarios, CUListadoUsuarios>();
builder.Services.AddScoped<IDevolverPrestamo, CUDevolverPrestamo>();
builder.Services.AddScoped<IListadoPrestamosActivosUsuario, CUListadoPrestamosActivosUsuario>();
builder.Services.AddScoped<IEvaluarObservacion, CUEvaluarObservacion>();
builder.Services.AddScoped<IListadoObjetosCelestes, CUListadoObjetosCelestes>();
builder.Services.AddScoped<ICrearObservacion, CUCrearObservacion>();
builder.Services.AddScoped<IListadoPrestamosSocioEntreFechas, CUListadoPrestamosSocioEntreFechas>();
builder.Services.AddScoped<IListadoSociosPorTelescopio, CUListadoSociosPorTelescopio>();
builder.Services.AddScoped<IRankingObjetosCelestes, CURankingObjetosCelestes>();
builder.Services.AddScoped<IListadoAuditoriasPrestamos, CUListadoAuditoriasPrestamos>();
builder.Services.AddScoped<IBuscarAuditoriaPrestamo, CUBuscarAuditoriaPrestamo>();
builder.Services.AddScoped<IBuscarPrestamoPorId, CUBuscarPrestamoPorId>();
builder.Services.AddScoped<IListadoUsuariosCompleto, CUListadoUsuariosCompleto>();
builder.Services.AddScoped<IListadoPrestamosPorCoordinador, CUListadoPrestamosPorCoordinador>();

builder.Services.AddScoped<IEvaluadorObservacionIA, EvaluadorObservacionIAGemini>();
//builder.Services.AddScoped<IEvaluadorObservacionIA, EvaluadorObservacionIAFake>();

string conBD = "";

if (builder.Environment.IsDevelopment())
{
    conBD = builder.Configuration.GetConnectionString("StellarMindsDB");
}

if (builder.Environment.IsProduction())
{
    conBD = builder.Configuration.GetConnectionString("StellarMindsProduccion");
}

builder.Services.AddDbContext<StellarMindsContext>(options =>
    options.UseSqlServer(conBD));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
