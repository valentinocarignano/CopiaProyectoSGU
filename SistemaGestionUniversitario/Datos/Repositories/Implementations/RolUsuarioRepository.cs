using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;
using System.Linq.Expressions;

namespace Datos.Repositories.Implementations
{
    public class RolUsuarioRepository : Repository<RolUsuario>, IRolUsuarioRepository
    {
        public RolUsuarioRepository(ApplicationDbContext context) : base(context)
        {             
        }

        public override async Task<IEnumerable<RolUsuario>> FindAllAsync()
        {
            return await _context.RolUsuario
                .Include(u => u.Usuarios)
                .ToListAsync();
        }

        public override async Task<IEnumerable<RolUsuario>> FindByConditionAsync(Expression<Func<RolUsuario, bool>> expression)
        {
            return await _context.RolUsuario
                .Include(u => u.Usuarios)
                .Where(expression)
                .ToListAsync();
        }
    }
}