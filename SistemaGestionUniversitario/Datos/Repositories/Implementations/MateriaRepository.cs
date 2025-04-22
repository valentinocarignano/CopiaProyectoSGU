using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;
using System.Linq.Expressions;

namespace Datos.Repositories.Implementations
{
    public class MateriaRepository : Repository<Materia>, IMateriaRepository
    {
        public MateriaRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override async Task<IEnumerable<Materia>> FindAllAsync()
        {
            return await _context.Materia
                .Include(m => m.Profesores)
                .ThenInclude(p => p.Usuario)
                .Include(m => m.DiaHorario)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Materia>> FindByConditionAsync(Expression<Func<Materia, bool>> expression)
        {
            return await _context.Materia
                .Include(m => m.Profesores)
                .ThenInclude(p => p.Usuario)
                .Include(m => m.DiaHorario)
                .Where(expression)
                .ToListAsync();
        }
    }
}