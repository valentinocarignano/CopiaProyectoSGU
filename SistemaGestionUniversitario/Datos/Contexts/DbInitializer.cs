using Entidades.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datos.Contexts
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            if (!await context.RolUsuario.AnyAsync())
            {
                var roles = new RolUsuario[]
                {
                    new RolUsuario { Descripcion = "Administrador" },
                    new RolUsuario { Descripcion = "Alumno" },
                    new RolUsuario { Descripcion = "Profesor" },

                };
                await context.RolUsuario.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            if (!await context.Dia.AnyAsync())
            {
                var dias = new Dia[]
                {
                    new Dia { Descripcion = "Lunes" },
                    new Dia { Descripcion = "Martes" },
                    new Dia { Descripcion = "Miércoles" },
                    new Dia { Descripcion = "Jueves" },
                    new Dia { Descripcion = "Viernes" }
                };

                await context.Dia.AddRangeAsync(dias);
                await context.SaveChangesAsync();
            }

            if (!await context.Horario.AnyAsync())
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

                await context.Horario.AddRangeAsync(horarios);
                await context.SaveChangesAsync();
            }

            if (!await context.DiaHorario.AnyAsync())
            {
                var diaHorarios = new DiaHorario[]
                {
                    new DiaHorario { IdDia = 1, IdHorario = 1 },
                    new DiaHorario { IdDia = 1, IdHorario = 2 },
                    new DiaHorario { IdDia = 1, IdHorario = 3 },
                    new DiaHorario { IdDia = 2, IdHorario = 4 },
                    new DiaHorario { IdDia = 2, IdHorario = 5 },
                    new DiaHorario { IdDia = 2, IdHorario = 6 },
                    new DiaHorario { IdDia = 3, IdHorario = 7 },
                    new DiaHorario { IdDia = 4, IdHorario = 8 },
                    new DiaHorario { IdDia = 5, IdHorario = 9 },
                    new DiaHorario { IdDia = 5, IdHorario = 10 },
                    new DiaHorario { IdDia = 5, IdHorario = 11 }
                };

                await context.DiaHorario.AddRangeAsync(diaHorarios);
                await context.SaveChangesAsync();
            }

        }
    }
}