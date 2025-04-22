using Entidades.Entities;

namespace Datos.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.RolUsuario.Any())
            {
                return; // La tabla Rol Usuario fue inicializada
            }
            else
            {
                var roles = new RolUsuario[]
                {
                new RolUsuario { Descripcion = "Administrador" },
                new RolUsuario { Descripcion = "Alumno" },
                new RolUsuario { Descripcion = "Profesor" },
                };

                context.RolUsuario.AddRangeAsync(roles).Wait();
                context.SaveChangesAsync();
            }

            if (context.Dia.Any())
            {
                return; // La tabla dia fue inicializada
            }
            else
            {
                var dias = new Dia[]
                {
                new Dia { Descripcion = "Lunes" },
                new Dia { Descripcion = "Martes" },
                new Dia { Descripcion = "Miércoles" },
                new Dia { Descripcion = "Jueves" },
                new Dia { Descripcion = "Viernes" }
                };

                context.Dia.AddRangeAsync(dias).Wait();
                context.SaveChanges();
            }

            if (context.Horario.Any())
            {
                return; // La tabla horario fue inicializada
            }
            else
            {
                var horarios = new Horario[]
                {
                new Horario { Descripcion = "16:20 - 17:00"},
                new Horario { Descripcion = "17:05 - 17:45"},
                new Horario { Descripcion = "17:55 - 18:35"},
                new Horario { Descripcion = "18:45 - 19:25"},
                new Horario { Descripcion = "19:35 - 20:15"},
                new Horario { Descripcion = "20:20 - 21:00"},
                new Horario { Descripcion = "16:20 - 18:35"},
                new Horario { Descripcion = "18:45 - 21:00"},
                new Horario { Descripcion = "15:30 - 17:00"},
                new Horario { Descripcion = "17:00 - 18:35"},
                new Horario { Descripcion = "18:45 - 21:00"},
                };

                context.Horario.AddRangeAsync(horarios).Wait();
                context.SaveChanges();
            }

            if (context.DiaHorario.Any())
            {
                return; // La tabla DiaHorario fue inicializada
            }
            else
            {
                var diaHorarios = new DiaHorario[]
                {
                    new DiaHorario { IdDia = 1, IdHorario = 1 },  // Lunes - 16:20 - 17:00
                    new DiaHorario { IdDia = 1, IdHorario = 2 },  // Lunes - 17:05 - 17:45
                    new DiaHorario { IdDia = 1, IdHorario = 3 },  // Lunes - 17:55 - 18:35
                    new DiaHorario { IdDia = 2, IdHorario = 4 },  // Martes - 18:45 - 19:25
                    new DiaHorario { IdDia = 2, IdHorario = 5 },  // Martes - 19:35 - 20:15
                    new DiaHorario { IdDia = 2, IdHorario = 6 },  // Martes - 20:20 - 21:00
                    new DiaHorario { IdDia = 3, IdHorario = 7 },  // Miércoles - 16:20 - 18:35
                    new DiaHorario { IdDia = 4, IdHorario = 8 },  // Jueves - 18:45 - 21:00
                    new DiaHorario { IdDia = 5, IdHorario = 9 },  // Viernes - 15:30 - 17:00
                    new DiaHorario { IdDia = 5, IdHorario = 10 }, // Viernes - 17:00 - 18:35
                    new DiaHorario { IdDia = 5, IdHorario = 11 }  // Viernes - 18:45 - 21:00
                };

                context.DiaHorario.AddRangeAsync(diaHorarios).Wait();
                context.SaveChanges();
            }

            //TODO: ACOMODAR DIAHORARIOS
        }
    }
}
