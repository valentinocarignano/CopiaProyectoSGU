using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IRolUsuarioRepository : IRepository<RolUsuario>
    {
        new Task<IEnumerable<RolUsuario>> FindAllAsync();
        new Task<IEnumerable<RolUsuario>> FindByConditionAsync(Expression<Func<RolUsuario, bool>> expression);
    }
}