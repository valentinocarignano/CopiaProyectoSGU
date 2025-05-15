using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class AsistenciaRepository : Repository<Asistencia>, IAsistenciaRepository
    {
        public AsistenciaRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<IEnumerable<Asistencia>> FindAllWithIncludesAsync()
        //{
        //    return await _context.Asistencia
        //        .Include(a => a.Inscripcion)
        //            .ThenInclude(i => i.Alumno)
        //                .ThenInclude(al => al.Usuario)
        //        .Include(a => a.Inscripcion)
        //            .ThenInclude(i => i.Materia)
        //        .Include(a => a.DiaHorarioMateria)
        //            .ThenInclude(dhm => dhm.DiaHorario)
        //                .ThenInclude(dh => dh.Dia)
        //        .Include(a => a.DiaHorarioMateria)
        //            .ThenInclude(dhm => dhm.DiaHorario)
        //                .ThenInclude(dh => dh.Horario)
        //        .ToListAsync();
        //}

    }
}
