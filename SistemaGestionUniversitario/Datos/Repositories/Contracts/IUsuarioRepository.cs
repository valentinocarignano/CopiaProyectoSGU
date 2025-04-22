using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        new Task<IEnumerable<Usuario>> FindAllAsync();
        new Task<IEnumerable<Usuario>> FindByConditionAsync(Expression<Func<Usuario, bool>> expression);
    }
}