using Datos.Contexts;
using Datos.Repositories.Contracts;
using Datos.Repositories.Implementations;
using Logica.Contracts;
using Logica.Implementations;
using Microsoft.EntityFrameworkCore;
using Negocio.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region Inyeccion de Dependencias de Repositorios
builder.Services.AddTransient<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddTransient<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddTransient<IDiaHorarioMateriaRepository, DiaHorarioMateriaRepository>();
builder.Services.AddTransient<IDiaHorarioRepository, DiaHorarioRepository>();
builder.Services.AddTransient<IDiaRepository, DiaRepository>();
builder.Services.AddTransient<IExamenRepository, ExamenRepository>();
builder.Services.AddTransient<IHorarioRepository, HorarioRepository>();
builder.Services.AddTransient<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddTransient<IMateriaRepository, MateriaRepository>();
builder.Services.AddTransient<INotaAlumnoRepository, NotaAlumnoRepository>();
builder.Services.AddTransient<IProfesorMateriaRepository, ProfesorMateriaRepository>();
builder.Services.AddTransient<IProfesorRepository, ProfesorRepository>();
builder.Services.AddTransient<IRolUsuarioRepository, RolUsuarioRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
#endregion

#region Inyeccion de Dependencias de Logica
builder.Services.AddTransient<IAlumnoLogic, AlumnoLogic>();
builder.Services.AddTransient<IAsistenciaLogic, AsistenciaLogic>();
builder.Services.AddTransient<IDiaHorarioMateriaLogic, DiaHorarioMateriaLogic>();
builder.Services.AddTransient<IDiaHorarioLogic, DiaHorarioLogic>();
builder.Services.AddTransient<IDiaLogic, DiaLogic>();
builder.Services.AddTransient<IExamenLogic, ExamenLogic>();
builder.Services.AddTransient<IHorarioLogic, HorarioLogic>();
builder.Services.AddTransient<IInscripcionLogic, InscripcionLogic>();
builder.Services.AddTransient<IMateriaLogic, MateriaLogic>();
builder.Services.AddTransient<INotaAlumnoLogic, NotaAlumnoLogic>();
builder.Services.AddTransient<IProfesorMateriaLogic, ProfesorMateriaLogic>();
builder.Services.AddTransient<IProfesorLogic, ProfesorLogic>();
builder.Services.AddTransient<IRolUsuarioLogic, RolUsuarioLogic>();
builder.Services.AddTransient<IUsuarioLogic, UsuarioLogic>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    await DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();