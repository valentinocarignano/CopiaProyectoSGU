using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;
using System.Linq.Expressions;

namespace Datos.Repositories.Implementations
{
    public class InscripcionRepository : Repository<Inscripcion>, IInscripcionRepository
    {
        public InscripcionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Inscripcion>> FindAllAsync()
        {
            return await _context.Inscripcion
                .Include(u => u.DiaHorarioMaterias)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Inscripcion>> FindByConditionAsync(Expression<Func<Inscripcion, bool>> expression)
        {
            return await _context.Inscripcion
                .Include(u => u.DiaHorarioMaterias)
                .Where(expression)
                .ToListAsync();
        }
    }
}
