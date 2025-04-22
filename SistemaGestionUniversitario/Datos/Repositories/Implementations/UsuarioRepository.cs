using Microsoft.EntityFrameworkCore;
using Entidades.Entities;
using Datos.Repositories.Contracts;
using Datos.Contexts;
using System.Linq.Expressions;

namespace Datos.Repositories.Implementations
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Usuario>> FindAllAsync()
        {
            return await _context.Usuario
                .Include(u => u.RolUsuario)
                .Include(u => u.Profesores)
                .Include(u => u.Alumnos)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Usuario>> FindByConditionAsync(Expression<Func<Usuario, bool>> expression)
        {
            return await _context.Usuario
                .Include(u => u.RolUsuario)
                .Include(u => u.Profesores)
                .Include(u => u.Alumnos)
                .Where(expression)
                .ToListAsync();
        }
    }
}