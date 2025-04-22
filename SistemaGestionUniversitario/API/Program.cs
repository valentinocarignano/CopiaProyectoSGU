using Datos.Contexts;
using Datos.Repositories.Contracts;
using Datos.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddTransient<IImagenRepository, ImagenRepository>();
builder.Services.AddTransient<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddTransient<IMateriaRepository, MateriaRepository>();
builder.Services.AddTransient<INotaAlumnoRepository, NotaAlumnoRepository>();
builder.Services.AddTransient<IProfesorMateriaRepository, ProfesorMateriaRepository>();
builder.Services.AddTransient<IProfesorRepository, ProfesorRepository>();
builder.Services.AddTransient<IRolUsuarioRepository, RolUsuarioRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
#endregion

#region Inyeccion de Dependencias de Logica
//TODO: INYECTAR DEPENDENCIAS
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
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();