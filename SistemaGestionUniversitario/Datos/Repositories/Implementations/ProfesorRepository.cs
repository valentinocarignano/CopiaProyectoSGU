using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;
using System.Linq.Expressions;

namespace Datos.Repositories.Implementations
{
    public class ProfesorRepository : Repository<Profesor>, IProfesorRepository
    {
        public ProfesorRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public override async Task<IEnumerable<Profesor>> FindAllAsync()
        {
            return await _context.Profesor
                .Include(u => u.Usuario)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Profesor>> FindByConditionAsync(Expression<Func<Profesor, bool>> expression)
        {
            return await _context.Profesor
                .Include(u => u.Usuario)
                .Where(expression)
                .ToListAsync();
        }
    }
}