using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class RolUsuarioRepository : Repository<RolUsuario>, IRolUsuarioRepository
    {
        public RolUsuarioRepository(ApplicationDbContext context) : base(context)
        {             
        }
    }
}
